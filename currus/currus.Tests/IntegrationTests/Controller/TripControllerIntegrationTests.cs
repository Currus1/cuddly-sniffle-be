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
    }
}
