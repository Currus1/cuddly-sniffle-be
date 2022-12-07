using currus.Controllers;
using currus.Enums;
using System.Numerics;

namespace currus.Tests.UnitTests.Controller;

[TestFixture]
public class EnumControllerTest
{
    public string[] TripStates = { "Planned", "Ongoing", "Ended", "Cancelled" };
    public string[] VehicleTypes = { "Sedan", "SUV", "EV", "Van" };

    [Test]
    public void EnumController_GetTripState_ReturnsAllEnumValues()
    {
        var controller = new EnumController();
        var result = controller.GetTripStateEnum();
        Assert.That(TripStates.Length, Is.EqualTo(result.Length));
        Assert.That(TripStates[0], Is.EqualTo(result[0]));
        Assert.That(TripStates[1], Is.EqualTo(result[1]));
        Assert.That(TripStates[2], Is.EqualTo(result[2]));
        Assert.That(TripStates[3], Is.EqualTo(result[3]));
    }

    [Test]
    public void EnumController_GetVehicleType_ReturnsAllEnumValues()
    {
        var controller = new EnumController();
        var result = controller.GetVehicleTypeEnum();
        Assert.That(VehicleTypes.Length, Is.EqualTo(result.Length));
        Assert.That(VehicleTypes[0], Is.EqualTo(result[0]));
        Assert.That(VehicleTypes[1], Is.EqualTo(result[1]));
        Assert.That(VehicleTypes[2], Is.EqualTo(result[2]));
        Assert.That(VehicleTypes[3], Is.EqualTo(result[3]));
    }
}