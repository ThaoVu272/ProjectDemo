using BlazorProject.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using BlazorProject.Iservice;
using Xunit;

namespace BlazorProject.Pages
{
    public class StudentListBase : ComponentBase
    {
        
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        [Inject]
        public IStudentservice StudentService { get; set; }
      
        public bool getBranchesError;
        public bool shouldRender;
        protected override async Task OnInitializedAsync()
        {
            Students = (await StudentService.GetStudent());  
        }
       
    }
}
