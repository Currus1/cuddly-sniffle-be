using currus.Models;
using currus.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using currus.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace currus.Tests.UnitTests.Controller;
    
[TestFixture]
public class AuthControllerTests
{
    private AuthController _authController;
    private Mock<UserManager<User>> _userManager;
    private IConfiguration _configuration;

    [SetUp]
    public void SetUp()
    {
        var inMemorySettings = new Dictionary<string, string> {
        {"JwtConfig:Secret", "key"},
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        var store = new Mock<IUserStore<User>>();
        store.Setup(x => x.FindByIdAsync(It.IsAny<String>(), CancellationToken.None))
            .ReturnsAsync(GetUserTestData());

        _userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<String>()))
            .ReturnsAsync(GetUserTestData());

        _authController = new AuthController(_userManager.Object, _configuration);
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
    public async Task AuthController_Register_UserAlreadyExists()
    {
        var user = GetUserTestData();

        UserRegisterDto userDto = new UserRegisterDto(user.Name, user.Surname, user.Email, user.Birthdate, user.PhoneNumber);

        var actionResult = await _authController.Register(userDto);

        var badRequest = actionResult as BadRequestObjectResult;
        var result = badRequest.Value as AuthResult;

        var auth = new AuthResult()
        {
            Token = null,
            Errors = new List<string>()
            {
                "User already exists! Please log in"
            },
            Result = false
        };

        Assert.That(result.Token, Is.EqualTo(auth.Token));
        Assert.That(result.Errors, Is.EqualTo(auth.Errors));
        Assert.That(result.Result, Is.EqualTo(auth.Result));
    }
}
