using currus.Models;
using currus.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;

using Assert = NUnit.Framework.Assert;

namespace currus.Tests.IntegrationTests.Controller
{
    [TestFixture]
    class UserControllerIntegrationTests
    {
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var factory = new MemoryFactory();
            _client = factory.CreateClient();
        }

        //[TestCategory("Integration")]
        //[Test]
        //public async Task Integration_UserController_GetUser_ReturnUserOk()
        //{
        //    var userId = 1;
        //    var name = "Abel";

        //    var response = await _client.GetAsync($"/user/{userId}");
        //    var user = await response.Content.ReadFromJsonAsync<User>();
            
        //    response.EnsureSuccessStatusCode();
        //    Assert.IsNotNull(response);
        //    Assert.That(user.Id, Is.EqualTo(userId));
        //    Assert.That(user.UserName, Is.EqualTo(name));
        //}
    }
}
