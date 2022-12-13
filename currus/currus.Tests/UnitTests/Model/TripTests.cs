using currus.Enums;
using currus.Migrations;
using currus.Models;
using Newtonsoft.Json;
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
        _trip = new Trip
        {
            Id = 1,
            SLatitude = 50,
            SLongitude = 40,
            DLatitude = 60,
            DLongitude = 50,
            StartingPoint = "Test1",
            Destination = "Test2",
            Seats = 4,
            Hours = 1,
            Minutes = 30,
            Distance = 100,
            DriverId = "123",
            VehicleType = "SUV",
            TripStatus = "Planned",
            EstimatedTripPrice = 10,
            TripDate = new DateTime()
        };
    }

    [Test]
    public void TripConstructor_Empty_ShouldCreateTrip()
    {
        Trip trip = new Trip();

        Assert.That(trip, Is.Not.Null);
    }

    [Test]
    public void TripConstructor_NotEmpty_ShouldCreateTrip()
    {
        var sLatitude = 50;
        var sLongitude = 40;
        var dLatitude = 60;
        var dLongitude = 50;
        var startingPoint = "Test1";
        var destination = "Test2";
        var seats = 4;
        var estimatedTripPrice = 10;
        var tripDate = new DateTime();

        Trip trip = new Trip(sLatitude, sLongitude, dLatitude,
            dLongitude, startingPoint, destination, estimatedTripPrice, seats, tripDate);

        Assert.That(trip, Is.Not.Null);
    }

    [Test]
    public void TripConstructor_NotEmpty_TripContentEqual()
    {
        var testTrip = new Trip(_trip.SLatitude, _trip.SLongitude, _trip.DLatitude,
            _trip.DLongitude, _trip.StartingPoint, _trip.Destination,
            _trip.EstimatedTripPrice, _trip.Seats, _trip.TripDate);

        testTrip.Id = _trip.Id;
        testTrip.TripStatus = _trip.TripStatus;
        testTrip.DriverId = _trip.DriverId;
        testTrip.VehicleType = _trip.VehicleType;
        testTrip.Hours = _trip.Hours;
        testTrip.Minutes = _trip.Minutes;
        testTrip.Distance = _trip.Distance;


        var result = JsonConvert.SerializeObject(testTrip);
        var trip = JsonConvert.SerializeObject(_trip);

        Assert.That(result, Is.EqualTo(trip));
    }
}