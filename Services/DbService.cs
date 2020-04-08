using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Ex3V2.Services
{

    public class DbService : IDbService
    {

        public bool CIndex(string index)
        {
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s18683;Integrated Security=True"))
              using (var com = new SqlCommand())
              {

                com.Connection = client;
                client.Open();
                com.CommandText = "select * from student where IndexNumber=@index";
                com.Parameters.AddWithValue("index", index);


                var dr = com.ExecuteReader();

                if (!dr.Read()){

                    return false;
                }
              }

         return true;
        }
    }
}
