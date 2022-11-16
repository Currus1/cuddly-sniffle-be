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
    private Lazy<IUserDbRepository> _userDbRepository;
    private UserController _userController;
    private Lazy<User> _user;
    private Lazy<int> _id;

    [SetUp]
    public void SetUp()
    {
        _user = new Lazy<User>(() => A.Fake<User>());
        _id = new Lazy<int>(() => 0); 
        _userDbRepository = new Lazy<IUserDbRepository>(() => A.Fake<IUserDbRepository>());
        _userController = new UserController(_userDbRepository.Value);
    }

    [Test]
    public void UserController_GetUser_ReturnUserOk()
    {
        A.CallTo(() => _userDbRepository.Value.Get(_id.Value)).Returns(_user.Value);

        var result = _userController.GetUser(_id.Value);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_AddUser_ReturnUserOk()
    {   
        var result = _userController.AddUser(_user.Value);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_DeleteUser_ReturnUserOk()
    {
        var result = _userController.DeleteUser(_user.Value);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_UpdateUser_ReturnUserOk()
    {
        var result = _userController.UpdateUser(_user.Value);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_DeleteUserById_ReturnUserOk()
    {
        var result = _userController.DeleteUserById(_id.Value);

        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Test]
    public void UserController_GetAllTrips_ReturnListOfTrips()
    {
        var numberOfFakes = 2;
        var trips = A.CollectionOfFake<Trip>(numberOfFakes);
        A.CallTo(() => _userDbRepository.Value.GetAllTrips(_id.Value)).Returns(trips);

        var result = _userController.GetAllTrips(_id.Value);

        result.Should().BeOfType<List<Trip>>();
        result.Should().NotBeEmpty();
    }

    [Test]
    public void UserController_SetRelation_ReturnOk()
    {
        var tripId = 0;

        A.CallTo(() => _userDbRepository.Value.SetRelation(_id.Value, tripId)).Returns(_user.Value);

        var result = _userController.SetRelation(_id.Value, tripId);

        result.Should().BeOfType<Task<IActionResult>>();
    }
}