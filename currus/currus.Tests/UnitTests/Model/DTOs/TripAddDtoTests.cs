using currus.Models.DTOs;

namespace currus.Tests.UnitTests.Model.DTOs
{
    [TestFixture]
    internal class TripAddDtoTests
    {
        [Test]
        public void TripAddDto_Constructor_ShouldCreateTripAddDto()
        {
            TripAddDto tripAddDto = new TripAddDto(1, 1, 1, 1, "Start", "Dest", 1, 1, new DateTime());

            Assert.IsNotNull(tripAddDto);
        }
    }
}
