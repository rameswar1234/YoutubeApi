using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using YoutubeApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoutubeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthController>
        [HttpPost]
        public string Post([FromBody] LogIn l)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source =.\SQLEXPRESS; Initial Catalog = YouTubeDb; Integrated Security = true; "))

            {
                using (SqlCommand cmd = new SqlCommand("select * from UserTable where email=@email", con))
                {

                    try
                    {

                        cmd.Parameters.AddWithValue("@email", l.email);
                        LogIn UserDetails = new LogIn();
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                           
                            UserDetails.email = reader["email"].ToString();
                            UserDetails.password = reader["password"].ToString();
                        
                        }
                        if (l.password == UserDetails.password)
                        {
                            return "sucsess";
                        }
                        else
                        {
                            return "failed";
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
