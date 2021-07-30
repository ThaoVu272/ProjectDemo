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

namespace BlazorProject.Pages
{
    public class StudentListBase : ComponentBase
    {
        
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        //public IHttpClientFactory ClientFactory;
        public HttpClient http;
      
        public bool getBranchesError;
        public bool shouldRender;
       // public IHttpClientFactory _httpClient { get; set; };

        //private void SetupClient(System.Net.Http.HttpClient client, string contentType)
        //{
        //    client.BaseAddress = new Uri(_httpClient);
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
        //}
        protected override async Task OnInitializedAsync()
        {
            //Student = GetStudents();
            //Students = await http.GetAsync<IEnumerable<Student>>("https://localhost:44325/api/Students");
            //var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44325/api/Students");
            //try
            //{
            //    var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            //    var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            //    var client = httpClientFactory.CreateClient();
            //    var response = await client.SendAsync(request);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        using var responseStream = await response.Content.ReadAsStreamAsync();
            //        Students = await JsonSerializer.DeserializeAsync<List<Student>>(responseStream);
            //    }
            //    else
            //    {
            //        getBranchesError = true;
            //    }

            //    shouldRender = true;
            //}
            //catch(Exception ex)
            //{
            //    getBranchesError = true;
            //    shouldRender = false;
            //}
            // string baseUrl = "http://localhost:44325";
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = httpClientFactory.CreateClient("ResAPI");
            client.Timeout = TimeSpan.FromMinutes(5);
           

            // client.BaseAddress = new Uri(baseUrl);
            //var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            //client.DefaultRequestHeaders.Accept.Add(contentType);
            var message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri("http://localhost:44325/api/Students/GetStudents");
            client.DefaultRequestHeaders.Clear();
            
            HttpResponseMessage apiResponse = null;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
            try
            {

                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                //if (response.IsSuccessStatusCode)
                //{
                //    var stringData = response.Content.ReadAsStringAsync().Result;
                 Students = JsonConvert.DeserializeObject<IEnumerable<Student>>(apiContent);
                //}
                //else
                //{
                //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                //    getBranchesError = true;
                //    shouldRender = false;
                //}

            }
        catch(Exception ex)
            {

            }
            

        }

        async Task<IEnumerable<Student>> GetUsersAsync()
        {
            HttpClient client = new HttpClient();
            List<Student> users = new List<Student>();
            var response = client.GetAsync("http://localhost:44325/api/Students/GetStudents").Result;
            if (response.IsSuccessStatusCode)
            {
                var stringData = response.Content.ReadAsStringAsync().Result;
                Students = JsonConvert.DeserializeObject<IEnumerable<Student>>(stringData);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                getBranchesError = true;
                shouldRender = false;
            }
            return users;
        }

        private async Task SaveStudent()
        {

        }
    }
}
