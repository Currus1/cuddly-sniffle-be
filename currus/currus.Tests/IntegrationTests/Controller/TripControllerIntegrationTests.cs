using currus.Models;
using currus.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;

using Assert = NUnit.Framework.Assert;

namespace currus.Tests.IntegrationTests.Controller
{
    [TestFixture]
    class TripControllerIntegrationTests
    {
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var factory = new MemoryFactory();
            _client = factory.CreateClient();
        }

        [TestCategory("Integration")]
        [Test]
        public async Task Integration_UserController_GetUser_ReturnUserOk() // need authorization
        {
            var tripId = 1;
            var startingPoint = "Vilnius";

            var response = await _client.GetAsync($"/trip/{tripId}");
            var trip = await response.Content.ReadFromJsonAsync<Trip>();

            response.EnsureSuccessStatusCode();
            Assert.IsNotNull(response);
            Assert.That(trip.Id, Is.EqualTo(tripId));
            Assert.That(trip.StartingPoint, Is.EqualTo(startingPoint));
        }
    }
}
