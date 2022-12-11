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
public class UserControllerTests
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
}