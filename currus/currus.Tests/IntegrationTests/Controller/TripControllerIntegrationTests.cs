using currus.Models;
using currus.Tests.Services;
using FluentAssertions;
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

        //[TestCategory("Integration")]
        //[Test]
        //public async Task Integration_UserController_GetUser_ReturnUserOk() // need authorization
        //{
        //    var tripId = 1;
        //    var startingPoint = "Vilnius";
        //    var expectedTrip = new Trip
        //    {
        //        Id = tripId,
        //        StartingPoint = startingPoint,
        //        // Set the other properties of the expected Trip object here
        //    };

        //    var response = await _client.GetAsync($"/trip/{tripId}");
        //    var trip = await response.Content.ReadFromJsonAsync<Trip>();

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    trip.Should().BeEquivalentTo(expectedTrip);
        //}
    }
}
