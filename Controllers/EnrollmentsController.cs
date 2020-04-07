using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Ex3V2.Models;
using Ex3V2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ex3V2.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
      

     /*

        [HttpPost]
        public IActionResult EnrollStudent(Student stud)
        {
            string messege = _service.EnrollStudent(stud);
            if (messege.Equals("OK"))
            {
                return Ok("ok");
            }
            else
            {
                return BadRequest(messege);
            }

        }

        /*
        [HttpPost("promotions")]
        public IActionResult PromoteStudents(Enrollment en)
        {
            string messege = _service.PromoteStudent(en);
            if (messege.Equals("OK"))
            {
                return Ok("ok");
            }
            else
            {
                return BadRequest(messege);
            }

            /*
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18508;Integrated Security=True"))
            using (var com = new SqlCommand()) 
            {
                com.Connection = client;
                client.Open();
                com.CommandText = "zad2 @StudyName, @Semester";
                com.Parameters.AddWithValue("StudyName", en.Study);
                com.Parameters.AddWithValue("Semester", en.Semester);
                try
                {
                    com.ExecuteNonQuery();
                    //ts.Commit();
                }
                catch (Exception exc)
                {
                    //ts.Rollback();
                    return BadRequest(exc.Message);
                }
            }
            return Ok("OK");
        }
        */
        
    }
}