using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSW_bolnica.Controllers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PSW_bolnica.Controllers.Tests
{
    public class AppointmentControllerTest
    {
        private readonly WebApplicationFactory<PSW_bolnica.Startup> _factory;
        private readonly HttpClient _httpClient;

        public AppointmentControllerTest()
        {
            _factory = new WebApplicationFactory<PSW_bolnica.Startup>();
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task NewAppointment()
        {
            try
            {
                DateTime from = new DateTime();
                DateTime to = new DateTime().AddDays(2);
                var url = "http://localhost:55960/newAppointment/user/" + from + "/" + to + "/1";

                var response = await _httpClient.PostAsync(url, null);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                responseBody.ShouldNotBeEmpty();
            }
            catch (Exception e)
            {
            }
        }

        [Fact]
        public async Task Cancel ()
        {
            var url = "http://localhost:55960/cancel/11";

            var response = await _httpClient.PutAsync(url, null);

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            responseBody.Contains("11");
        }


    }
}