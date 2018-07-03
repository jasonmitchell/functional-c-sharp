using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Tests.Extensions;
using Web.Api;
using Web.Api.Application;
using Xunit;

namespace Tests
{
    public class StudiesControllerTests
    {
        private readonly TestServer _server;
        
        public StudiesControllerTests()
        {
            _server = new TestServer(
                new WebHostBuilder()
                .UseStartup<Startup>());
        }

        [Fact]
        public async Task StartStudyReturnsCreatedAndLocation()
        {
            var command = new StartStudy
            {
                Id = Guid.NewGuid().ToString("n"),
                Title = "Test Study",
                Description = "This is a test study"
            };
            
            var response = await _server.CreateClient().SendJsonPost("/studies", command);

            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Headers.Location.Should().Be($"/studies/{command.Id}");
        }

        [Fact]
        public async Task IncludeSlideReturnsCreatedAndLocation()
        {
            var command = new IncludeSlideInStudy
            {
                StudyId = Guid.NewGuid().ToString("n"),
                SlideId = Guid.NewGuid().ToString("n")
            };

            var response = await _server.CreateClient().SendJsonPost($"/studies/{command.StudyId}/slides", command);
            
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Headers.Location.Should().Be($"/studies/{command.StudyId}/slides/{command.SlideId}");
        }
    }
}