using currus.Models.DTOs;

namespace currus.Tests.UnitTests.Model.DTOs
{
    [TestFixture]
    internal class DriverDtoTests
    {
        [Test]
        public void DriverDto_Constructor_ShouldCreateDriverDto()
        {
            DriverDto driverDto = new DriverDto("John", "Johny", "John123@gmail.com",
                   new DateTime(), "868686869", "12345678", "SUV", "AAA111");

            Assert.IsNotNull(driverDto);
        }
    }
}
