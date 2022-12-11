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