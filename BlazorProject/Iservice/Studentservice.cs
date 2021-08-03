using BlazorProject.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace BlazorProject.Iservice
{
    public class Studentservice : IStudentservice
    {
        private const string BaseUrl = "https://localhost:44300/api/Students/GetStudents";
        private readonly HttpClient _client;

        public Studentservice(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Student>> GetStudent()
        {
            var httpResponse = await _client.GetAsync(BaseUrl);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }
            var content = await httpResponse.Content.ReadAsStringAsync();
            var task = JsonConvert.DeserializeObject<List<Student>>(content);
            return task;
        }


    }
}
