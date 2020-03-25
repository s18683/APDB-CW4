using Ex3V2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Ex3V2.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase{

        [HttpGet]
        public IActionResult GetStudent(){

            var list = new List<Student>();
            //Nie umielem sie odwolac do bazy szkolnej, caly czas dostawalem blad nie udalo sie zaglogowac/haslo jest zle
            using (var con = new SqlConnection("Server = localhost, 33333; Initial Catalog = s18683; User ID = admin; Password = admin"))
            using (var com = new SqlCommand()){
                com.Connection = con;
                com.CommandText = "SELECT * FROM student,enrollment,studies " + "WHERE student.idenrollment=enrollment.idenrollment AND studies.idstudy=enrollment.idstudy";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read()){

                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.StudiesName = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();
                    list.Add(st);
                }
            }
            return Ok(list);
        }

        [HttpGet("{indexNumber}")]
        public IActionResult GetStudent(string indexNumber){

            //Nie umielem sie odwolac do bazy szkolnej, caly czas dostawalem blad nie udalo sie zaglogowac/haslo jest zle
            using (SqlConnection con = new SqlConnection("Server = localhost, 33333; Initial Catalog = s18683; User ID = admin; Password = admin"))
            using (SqlCommand com = new SqlCommand()){

                com.Connection = con;
                com.CommandText = "select * from student, enrollment where indexnumber=@index AND " + "enrollment.idenrollment = student.idenrollment";
                com.Parameters.AddWithValue("index", indexNumber);

                con.Open();
                var dr = com.ExecuteReader();
                if (dr.Read()){

                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    return Ok($"{dr["FirstName"].ToString()} {dr["LastName"].ToString()} " + $"Semester = {dr["Semester"].ToString()} StartDate = {dr["StartDate"].ToString()}");
                }

            }
            return NotFound();
        }
    }
}
