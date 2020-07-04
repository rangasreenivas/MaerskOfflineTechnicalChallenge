using JobTrackingApi;
using JobTrackingApi.Controllers;
using JobTrackingApi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestJobTRack
{
    public class JobUnitTest
    {
        private readonly JobContext _context;
        private readonly HttpClient _client;

        public JobUnitTest()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            _context = server.Host.Services.GetService(typeof(JobContext)) as JobContext;
            _client = server.CreateClient();
        }
        [Fact]
        public async Task DoesReturnNotFound_GivenJobidDoesNotExist()
        {
            // Act
            var response = await _client.GetAsync($"/api/jobs/4"); // No users with ID abc

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DoesReturnOk_GivenJobIdExists()
        {
            // Arrange
            var job = new Job
            {
                JobId = 1,
                Status="Pending"
            };

            _context.JobList.Add(job);
            _context.SaveChanges();

            // Act
            var response = await _client.GetAsync($"/api/jobs/{job.JobId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string jsonResult = await response.Content.ReadAsStringAsync();
            Job userFromJson = JsonConvert.DeserializeObject<Job>(jsonResult);
            Assert.Equal(job.JobId, userFromJson.JobId);
            Assert.Equal(job.Status, userFromJson.Status);
        }
    }
}
