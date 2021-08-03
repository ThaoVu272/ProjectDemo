using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorProject.Data;

namespace BlazorProject.Iservice
{
    public interface IStudentservice
    {
        Task<List<Student>> GetStudent();
    }
}
