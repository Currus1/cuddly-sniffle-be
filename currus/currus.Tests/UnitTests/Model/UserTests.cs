using currus.Enums;
using currus.Models;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace currus.Tests.Model;

[TestFixture]
internal class UserTests
{
    private User _user;
    private string _phoneNumberPattern;
    private string _emailPattern;
    private string _licenseNumberPattern;

    [SetUp]
    public void SetUp()
    {
        _user = new User
        {
            Name = "Testas",
            Surname = "Testavicius",
            Birthdate = new DateTime(),
            Email = "test@email.com",
            PhoneNumber = "+37060355589",
            VehicleType = "Van",
            LicenseNumber = "TTT777",
            DriversLicense = "",
            Trips = new List<Trip>()
        };

        _phoneNumberPattern = @"^((86|\+3706)\d{7})$";
        _emailPattern = @"^([a-zA-Z0-9_\-\.]{1,64})@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4}|[0-9]{1,3})$";
        _licenseNumberPattern = @"^[A-Z]{3}\d{3}$";
    }

    [Test]
    public void UserConstructor_Empty_ShouldCreateUser()
    {
        User user = new User();

        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public void UserPhoneNumber_RightPatternWith86_ShouldSucceed()
    {
        _user.PhoneNumber = "868686868";

        var result = Regex.IsMatch(_user.PhoneNumber, _phoneNumberPattern);

        Assert.That(result, Is.True);
    }

    [Test]
    public void UserPhoneNumber_WrongPatternWith86_ShouldFail()
    {
        _user.PhoneNumber = "8686868682";

        var result = Regex.IsMatch(_user.PhoneNumber, _phoneNumberPattern);

        Assert.That(result, Is.False);
    }

    [Test]
    public void UserPhoneNumber_WrongPatternWith370_ShouldFail()
    {
        _user.PhoneNumber = "37066868686";

        var result = Regex.IsMatch(_user.PhoneNumber, _phoneNumberPattern);

        Assert.That(result, Is.False);
    }

    [Test]
    public void UserPhoneNumber_RightPatternWith370_ShouldSucceed()
    {
        _user.PhoneNumber = "+37066868686";

        var result = Regex.IsMatch(_user.PhoneNumber, _phoneNumberPattern);

        Assert.That(result, Is.True);
    }

    [Test]
    public void UserEmail_WrongPatternNotMachingLastString_ShouldFail()
    {
        _user.Email = "name@gmail.something";

        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.That(result, Is.False);
    }

    [Test]
    public void UserEmail_RightPattern_ShouldSucceed()
    {
        _user.Email = "name@gmail.com";
        
        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.That(result, Is.True);
    }

    [Test]
    public void UserEmail_WrongPatternNoAtSign_ShouldFail()
    {
        _user.Email = "namegmail.com";

        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.That(result, Is.False);
    }

    [Test]
    public void UserEmail_WrongPatternNotMachingLength_ShouldFail()
    {
        _user.Email = new string('a', 257) + "@gmail.com";

        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.That(result, Is.False);
    }

    [Test]
    public void UserEmail_WrongPatternNotMachingFormat_ShouldFail()
    {
        _user.Email = "namegmail.something";

        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.That(result, Is.False);
    }


    [Test]
    public void UserLicenseNumber_RightPattern_ShouldSucceed()
    {
        _user.LicenseNumber = "AAA111";

        var result = Regex.IsMatch(_user.LicenseNumber, _licenseNumberPattern);

        Assert.That(result, Is.True);
    }

    [Test]
    public void UserLicenseNumber_WrongPatternNotMatchingLength_ShouldSucceed()
    {
        _user.LicenseNumber = "AAA1112";

        var result = Regex.IsMatch(_user.LicenseNumber, _licenseNumberPattern);

        Assert.That(result, Is.False);
    }

    [Test]
    public void UserLicenseNumber_WrongPatternLetterAndNumbersSwitched_ShouldSucceed()
    {
        _user.LicenseNumber = "111AAA";

        var result = Regex.IsMatch(_user.LicenseNumber, _licenseNumberPattern);

        Assert.That(result, Is.False);
    }
}