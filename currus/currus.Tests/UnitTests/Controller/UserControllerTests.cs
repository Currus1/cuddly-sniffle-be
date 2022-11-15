using currus.Controllers;
using currus.Models;
using currus.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace currus.Tests.Controller;

[TestFixture]
public class UserControllerTests
{
    private IUserDbRepository _userDbRepository;
    private UserController _userController;

    [SetUp]
    public void SetUp()
    {
        _userDbRepository = A.Fake<IUserDbRepository>();

        _userController = new UserController(_userDbRepository);
    }

    [Test]
    public void UserController_GetUser_ReturnUserOk()
    {
        var id = 0;
        var user = A.Fake<User>();
        A.CallTo(() => _userDbRepository.Get(id)).Returns(user);

        var result = _userController.GetUser(id);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_AddUser_ReturnUserOk()
    {   
        var user = A.Fake<User>();

        var result = _userController.AddUser(user);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_DeleteUser_ReturnUserOk()
    {
        var user = A.Fake<User>();

        var result = _userController.DeleteUser(user);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_UpdateUser_ReturnUserOk()
    {
        var user = A.Fake<User>();

        var result = _userController.UpdateUser(user);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_DeleteUserById_ReturnUserOk()
    {
        var id = 0;

        var result = _userController.DeleteUserById(id);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_GetAllTrips_ReturnListOfTrips()
    {
        var id = 0;
        var numberOfFakes = 2;
        var trips = A.CollectionOfFake<Trip>(numberOfFakes);
        A.CallTo(() => _userDbRepository.GetAllTrips(id)).Returns(trips);

        var result = _userController.GetAllTrips(id);

        result.Should().BeOfType<List<Trip>>();
        result.Should().NotBeEmpty();
    }

    [Test]
    public void UserController_SetRelation_ReturnOk()
    {
        var id = 0;
        var tripId = 0;
        var user = A.Fake<User>();

        A.CallTo(() => _userDbRepository.SetRelation(id, tripId)).Returns(user);

        var result = _userController.SetRelation(id, tripId);

        result.Should().BeOfType<Task<IActionResult>>();
    }
}