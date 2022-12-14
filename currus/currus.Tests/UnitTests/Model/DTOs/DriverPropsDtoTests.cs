using currus.Models.DTOs;

namespace currus.Tests.UnitTests.Model.DTOs
{
    [TestFixture]
    internal class DriverPropsDtoTests
    {
        [Test]
        public void DriverPropsDto_Constructor_ShouldCreateDriverDto()
        {
            DriverPropsDto driverPropsDto = new DriverPropsDto("12345678", "SUV", "AAA111");

            Assert.IsNotNull(driverPropsDto);
        }
    }
}
