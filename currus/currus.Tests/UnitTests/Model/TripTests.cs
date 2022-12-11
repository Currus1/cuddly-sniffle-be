using currus.Enums;
using currus.Migrations;
using currus.Models;
using NUnit.Framework.Internal;
using System.Configuration;

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
        var id = 0;
        var sLatitude = 50;
        var sLongitude = 40;
        var dLatitude = 60;
        var dLongitude = 50;
        var startingPoint = "Test1";
        var destination = "Test2";
        var seats = 4;
        var hours = 1;
        var minutes = 2;
        var distance = 100;
        var driverId = "123";
        var vehicleType = "Van";
        var estimatedTripPrice = 10;
        var tripStatus = "Planned";

        Trip trip = new Trip(id, sLatitude, sLongitude, dLatitude,
            dLongitude, startingPoint, destination, seats, hours,
            minutes, distance, driverId, vehicleType,
            estimatedTripPrice, tripStatus);

        Assert.IsNotNull(trip);
    }

    [Test]
    public void TripConstructor_NotEmpty_TripContentEqual()
    {
        Trip testTrip = new Trip
        {
            Id = 0,
            SLatitude = 50,
            SLongitude = 40,
            DLatitude = 60,
            DLongitude = 50,
            StartingPoint = "Test1",
            Destination = "Test2",
            Seats = 4,
            Hours = 1,
            Minutes = 2,
            Distance = 100,
            DriverId = "123",
            VehicleType = "Van",
            EstimatedTripPrice = 10,
            TripStatus = "Planned",
            Users = new List<User>()
        };

        var result = new Trip(testTrip.Id, testTrip.SLatitude, testTrip.SLongitude, testTrip.DLatitude,
            testTrip.DLongitude, testTrip.StartingPoint, testTrip.Destination, testTrip.Seats,
            testTrip.Hours, testTrip.Minutes, testTrip.Distance, testTrip.DriverId,
            testTrip.VehicleType, testTrip.EstimatedTripPrice, testTrip.TripStatus);

        Assert.That(result.Id, Is.EqualTo(testTrip.Id));
        Assert.That(result.SLatitude, Is.EqualTo(testTrip.SLatitude));
        Assert.That(result.SLongitude, Is.EqualTo(testTrip.SLongitude));
        Assert.That(result.DLatitude, Is.EqualTo(testTrip.DLatitude));
        Assert.That(result.DLongitude, Is.EqualTo(testTrip.DLongitude));
        Assert.That(result.StartingPoint, Is.EqualTo(testTrip.StartingPoint));
        Assert.That(result.Destination, Is.EqualTo(testTrip.Destination));
        Assert.That(result.Seats, Is.EqualTo(testTrip.Seats));
        Assert.That(result.Hours, Is.EqualTo(testTrip.Hours));
        Assert.That(result.Minutes, Is.EqualTo(testTrip.Minutes));
        Assert.That(result.Distance, Is.EqualTo(testTrip.Distance));
        Assert.That(result.DriverId, Is.EqualTo(testTrip.DriverId));
        Assert.That(result.VehicleType, Is.EqualTo(testTrip.VehicleType));
        Assert.That(result.EstimatedTripPrice, Is.EqualTo(testTrip.EstimatedTripPrice));
        Assert.That(result.TripStatus, Is.EqualTo(testTrip.TripStatus));
        Assert.That(result.Users, Is.EqualTo(testTrip.Users));
    }
}