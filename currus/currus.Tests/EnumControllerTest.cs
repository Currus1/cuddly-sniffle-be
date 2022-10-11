using currus.Controllers;
using currus.Enums;

namespace currus.Tests;

[TestFixture]
public class EnumControllerTest
{
    public TripStatuses[] TripStates = { TripStatuses.Planned, TripStatuses.Ongoing, TripStatuses.Ended, TripStatuses.Cancelled};
    public VehicleTypes[] VehicleTypes = { Enums.VehicleTypes.Sedan, Enums.VehicleTypes.SUV, Enums.VehicleTypes.EV, Enums.VehicleTypes.Van};

    [Test]
    public void GetTripState_ReturnsAllEnumValues()
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
    public void GetVehicleType_ReturnsAllEnumValues()
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