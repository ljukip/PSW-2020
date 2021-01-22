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

namespace PSW_tests.Integration_tests
{
   public class FeedbackControllerTests
    {
        private readonly WebApplicationFactory<PSW_bolnica.Startup> _factory;
        private readonly HttpClient _httpClient;

        public FeedbackControllerTests()
        {
            _factory = new WebApplicationFactory<PSW_bolnica.Startup>();
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task PublishS()
        {
            var url = "http://localhost:55960/publish/2";

            var response = await _httpClient.PutAsync(url, null);

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            responseBody.Contains("proba2");
        }
    }
}
