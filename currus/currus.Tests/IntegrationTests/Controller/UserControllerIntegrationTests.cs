using currus.Controllers;
using currus.Data;
using currus.Models;
using currus.Repository;
using currus.Tests.Services;
using FakeItEasy;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;

using Assert = NUnit.Framework.Assert;

namespace currus.Tests.IntegrationTests.Controller
{
    [TestFixture]
    class UserControllerIntegrationTests
    {
        private IUserDbRepository _userDbRepository;
        private UserController _userController;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var factory = new MemoryFactory();
            _client = factory.CreateClient();

            _userDbRepository = A.Fake<IUserDbRepository>();
            _userController = new UserController(_userDbRepository);
        }

        [TestCategory("Integration")]
        [Test]
        public async Task Integration_UserController_GetUser_ReturnUserOk()
        {
            var userId = 1;

            var response = await _client.GetFromJsonAsync<User>($"/user/{userId}");

            Assert.IsNotNull(response);
            Assert.That(response.Name, Is.EqualTo("Abel"));
        }
    }
}
