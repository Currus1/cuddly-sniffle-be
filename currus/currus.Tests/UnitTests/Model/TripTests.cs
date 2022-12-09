using currus.Models;

namespace currus.Tests.Model;

[TestFixture]
internal class TripTests
{
    private Trip _trip;


    [SetUp]
    public void SetUp()
    {
        _trip = new Trip();
    }

    [Test]
    public void CalculateTripPrice_TimeIsNull_ShouldThrowArgumentOutOfRange()
    {
        var hours = 0;
        var minutes = 0;
        var distance = 10;

        Assert.Throws<ArgumentOutOfRangeException>(() => { _trip.CalculateTripPrice(hours, minutes, distance); });
    }

    [Test]
    public void CalculateTripPrice_DistanceIsNull_ShouldThrowArgumentOutOfRange()
    {
        var hours = 10;
        var minutes = 10;
        var distance = 0;

        Assert.Throws<ArgumentOutOfRangeException>(() => { _trip.CalculateTripPrice(hours, minutes, distance); });
    }

    [Test]
    public void CalculateTripPrice_InputIsValidOptArg_ShouldReturn13()
    {
        var hours = 1;
        var minutes = 10;
        var distance = 20;
        decimal expectedValue = 13m;

        Assert.That(expectedValue, Is.EqualTo(_trip.CalculateTripPrice(hours, minutes, distance)));
    }

    [Test]
    public void CalculateTripPrice_InputIsValid_ShouldReturn14()
    {
        var hours = 1;
        var minutes = 10;
        var distance = 20;
        var basePrice = 3;
        decimal expectedValue = 14m;

        Assert.That(expectedValue, Is.EqualTo(_trip.CalculateTripPrice(hours, minutes, distance, basePrice)));
    }

    [Test]
    public void CalculateBasePrice_InputIsSuv_ShouldReturn3()
    {
        _trip.VehicleType = "SUV";
        var expectedValue = 3;

        Assert.That(expectedValue, Is.EqualTo(_trip.CalculateBasePrice(_trip)));
    }
    [Test]
    public void CalculateBasePrice_InputIsVan_ShouldReturn3()
    {
        _trip.VehicleType = "Van";
        var expectedValue = 3;

        Assert.That(expectedValue, Is.EqualTo(_trip.CalculateBasePrice(_trip)));
    }

    [Test]
    public void CalculateBasePrice_InputIsEV_ShouldReturn1()
    {
        _trip.VehicleType = "EV";
        var expectedValue = 1;

        Assert.That(expectedValue, Is.EqualTo(_trip.CalculateBasePrice(_trip)));
    }

    [Test]
    public void CalculateBasePrice_InputIsSedan_ShouldReturn2()
    {
        _trip.VehicleType = "Sedan";
        var expectedValue = 2;

        Assert.That(expectedValue, Is.EqualTo(_trip.CalculateBasePrice(_trip)));
    }

    [Test]
    public void CalculateBasePrice_InputIsOtherType_ShouldReturn2()
    {
        _trip.VehicleType = "Minivan";
        var expectedValue = 2;

        Assert.That(expectedValue, Is.EqualTo(_trip.CalculateBasePrice(_trip)));
    }

    [Test]
    public void TripConstructor_Empty_ShouldCreateTrip()
    {
        Trip trip = new Trip();

        Assert.IsNotNull(trip);
    }

    [Test]
    public void TripConstructor_NotEmpty_ShouldCreateTrip()
    {
        
    }
}