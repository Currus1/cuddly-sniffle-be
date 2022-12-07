using currus.Controllers;
using currus.Models;
using currus.Models.DTOs;
using currus.Repository;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Common;
using HttpContextMoq;
using HttpContextMoq.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Moq;
using NUnit.Framework.Internal;


namespace currus.Tests.UnitTests.Controller;

[TestFixture]
public class UserControllerTest
{
    private UserController _userController;
    private Mock<UserManager<User>> _userManager;
    private Lazy<ITripDbRepository> _tripDbRepository;

    [SetUp]
    public void SetUp()
    {

        //_httpContextAccessor = A.Fake<HttpContextAccessor>();
        //new Lazy<UserManager<User>>(() => A.Fake<UserManager<User>>());
        //var store = new Mock<IUserStore<User>> ();
        //store.Setup(x => x.FindByIdAsync("123", CancellationToken.None)).ReturnsAsync(GetUserTestData());
        //storet.Setup(x => x.FindByEmailAsync("test@email.com")).ReturnsAsync(GetUserTestData());


        var store = new Mock<IUserStore<User>>();
        store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
            .ReturnsAsync(GetUserTestData());

        _userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        _userManager.Setup(x => x.FindByEmailAsync("test@email.com"))
            .ReturnsAsync(GetUserTestData());

        _tripDbRepository = new Lazy<ITripDbRepository>(() => A.Fake<ITripDbRepository>());
        _userController = new UserController(_userManager.Object, null, _tripDbRepository.Value);
    }


    private User GetUserTestData()
    {
        var user = new User()
        {
            Id = "123",
            Name = "Testas",
            Surname = "Testavicius",
            Birthdate = DateTime.Now,
            Email = "test@email.com",
            PhoneNumber = "+37060670750",
            VehicleType = null,
            LicenseNumber = null,
            Trips = new List<Trip>()
        };

        return user;
    }

    [Test]
    public async Task UserController_UserManager_FindByEmailIsEqual()
    {
        User userTest = GetUserTestData();
        var manager = _userManager.Object;

        var result = await manager.FindByEmailAsync(userTest.Email);

        Assert.IsNotNull(result);
        Assert.That(result.Email, Is.EqualTo(userTest.Email));
    }

    [Test]
    public async Task UserController_GetUser_ReturnUserOk()
    {
        User expected = GetUserTestData();

        IActionResult actionResult = await _userController.GetUser();

        OkObjectResult result = actionResult as OkObjectResult;

        Assert.IsInstanceOf<BadRequestResult>(actionResult);
        Assert.IsNotNull(result);
        Assert.That(result.Value, Is.EqualTo(200));
        Assert.That(result.Value, Is.EqualTo(expected));
    }

    /*[Test]
    public void UserController_AddUser_ReturnUserOk()
    {   
        var result = _userController.AddUser(_user.Value);

        result.Should().BeOfType<Task<IActionResult>>();
    }*/

    /*[Test]
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
    }*/
}