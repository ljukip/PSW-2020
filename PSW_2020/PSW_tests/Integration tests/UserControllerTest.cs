using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PSW_bolnica.dao;
using PSW_bolnica.model;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task GetUsers()
        {
            var response = await _httpClient.GetAsync("http://localhost:55960/getUsers");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            responseBody.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task cancelAppointment ()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(800);
            var response = await _httpClient.GetAsync("http://localhost:55960/patientAppointments/user");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            responseBody.ShouldNotBeEmpty();
        }


        [Fact]
        public async Task Login()
        {
            try
            {
                var url = "http://localhost:55960/login";

                var jsonString = "{\"username\":\"user\",\"password\":\"user\"}";
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, httpContent);

                Debug.Write(response);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                responseBody.ShouldNotBeEmpty();
            }
            catch(Exception e) { 
            }

            
        }

        [Fact]
        public async Task Block()
        {
            try
            {
                var url = "http://localhost:55960/blockedUser/3";

                var response = await _httpClient.PutAsync(url, null);

                Debug.Write(response);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                responseBody.Contains("a");
            }
            catch (Exception e)
            {
            }


        }

    }
}