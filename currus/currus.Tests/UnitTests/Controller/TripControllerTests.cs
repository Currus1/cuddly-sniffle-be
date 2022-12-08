using currus.Controllers;
using currus.Models;
using currus.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace currus.Tests.Controller;

[TestFixture]
public class TripControllerTests
{
    private TripController _tripController;
    private Lazy<Trip> _trip;
    private Lazy<int> _id;
    private Lazy<ITripDbRepository> _tripDbRepository;

    [SetUp]
    public void SetUp()
    {
        UserManager<User> manager = A.Fake<UserManager<User>>();
        _trip = new Lazy<Trip>(() => A.Fake<Trip>());
        _id = new Lazy<int>(() => 0);
        _tripDbRepository = new Lazy<ITripDbRepository>(() => A.Fake<ITripDbRepository>());
        _tripController = new TripController(_tripDbRepository.Value, manager);
    }

    [Test]
    public void UserController_GetUser_ReturnUserOk()
    {
        A.CallTo(() => _tripDbRepository.Value.Get(_id.Value)).Returns(_trip.Value);

        var result = _tripController.GetTrip(_id.Value);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void TripController_AddTrip_ReturnTripOk()
    {
        var result = _tripController.AddTrip(_trip.Value);

        result.Should().BeOfType<Task<OkObjectResult>>();
        var okResult = result.Result as OkObjectResult;
        Assert.IsTrue((bool)okResult.Value);
    }

    [Test]
    public void TripController_DeleteTrip_ReturnTripOk()
    {
        // Act
        var result = _tripController.DeleteTrip(_trip.Value);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        Assert.IsTrue((bool)okResult.Value);
    }

    [Test]
    public void TripController_UpdateTrip_ReturnTripOk()
    {
        // Act
        var result = _tripController.UpdateTrip(_trip.Value);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        Assert.IsTrue((bool)okResult.Value);
    }

    [Test]
    public void TripController_DeleteTripById_ReturnTripOk()
    {
        // Act
        var result = _tripController.DeleteTripById(_id.Value);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        Assert.IsTrue((bool)okResult.Value);
    }


    [Test]
    public void UserController_SetRelation_ReturnOk()
    {
        var tripId = 0;

        A.CallTo(() => _tripDbRepository.Value.SetRelation(_id.Value, tripId)).Returns(_trip.Value);

        var result = _tripController.SetRelation(_id.Value, tripId);

        result.Should().BeOfType<Task<IActionResult>>();
    }
}