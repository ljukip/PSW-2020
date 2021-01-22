using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PSW_tests.Integration_tests
{
    public class UserControllerTest
    {
        private readonly WebApplicationFactory<PSW_bolnica.Startup> _factory;
        private readonly HttpClient _httpClient;

        public UserControllerTest()
        {
            _factory = new WebApplicationFactory<PSW_bolnica.Startup>();
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task Login()
        {
            DateTime from = new DateTime();
            DateTime to = new DateTime().AddDays(2);
            var response = await _httpClient.PostAsync("http://localhost:55960/newAppointment/user/from/to/1", new StringContent("", Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            responseBody.ShouldBe("{\"statusCode\":200}");
        }

    }
}