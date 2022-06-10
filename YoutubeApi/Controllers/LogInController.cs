using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using YoutubeApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YoutubeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        // GET: api/<LogInControlor>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LogInControlor>/5
        [HttpGet("{email}")]
        public string Get(string email)
        {
            return "";
        }

        // POST api/<LogInControlor>
        [HttpPost]
        public IActionResult Post([FromBody] LogIn logIn)
        {
            using (SqlConnection con = new SqlConnection(@"Data Source =.\SQLEXPRESS; Initial Catalog = YouTubeDb; Integrated Security = true; "))

            {
                using (SqlCommand cmd = new SqlCommand("Insert into UserTable Values(@email,@password,@firstName,@lastName)", con))
                {

                    try
                    {
                        cmd.Parameters.AddWithValue("@firstName", logIn.firstName);
                        cmd.Parameters.AddWithValue("@password", logIn.password);
                        cmd.Parameters.AddWithValue("@lastName", logIn.lastName);
                        cmd.Parameters.AddWithValue("@email", logIn.email);

                        con.Open();
                        int count = cmd.ExecuteNonQuery();
                        

                        if (count > 0)
                        {
                            return Ok("Data Inserted Successfully!") ;
                        }
                        else
                        {
                            return Ok ("Insertion Failed!");
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

        // PUT api/<LogInControlor>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogInControlor>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
