using System.Collections.Generic;
using Ex3V2.Models;

namespace Ex3V2.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}