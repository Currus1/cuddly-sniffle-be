using currus.Models;

namespace currus.Tests;

[TestFixture]
internal class TripTest
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

}