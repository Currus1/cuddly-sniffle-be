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
        _user = new User();
        _phoneNumberPattern = @"^((86|\+3706)\d{7})$";
        _emailPattern = @"^([a-zA-Z0-9_\-\.]{1,64})@(([a-zA-Z0-9\-]+\.)+)([a-zA-Z]{2,4}|[0-9]{1,3})$";
        _licenseNumberPattern = @"^[A-Z]{3}\d{3}$";
    }

    [Test]
    public void UserConstructor_Empty_ShouldCreateUser()
    {
        User user = new User();

        Assert.IsNotNull(user);
    }

    [Test]
    public void UserConstructor_NotEmpty_ShouldCreateUser()
    {
        var name = "Name";
        var surname = "Surname";
        var birthdate = new DateTime();
        var email = "name@gmail.com";
        var phoneNumber = "868686868";
        var vehicleType = "SUV";
        var licenseNumber = "AAA111";
       
        User user = new User(name, surname, birthdate, email, 
            phoneNumber, vehicleType, licenseNumber);

        Assert.IsNotNull(user);
    }

    [Test]
    public void UserConstructor_NotEmpty_UserContentEqual()
    {
        User testUser = new User
        {
            Name = "Name",
            Surname = "Surname",
            Birthdate = new DateTime(),
            Email = "name@gmail.com",
            PhoneNumber = "868686868",
            VehicleType = "SUV",
            LicenseNumber = "AAA111",
            Trips = new List<Trip>()
        };

        var result = new User(testUser.Name, testUser.Surname, testUser.Birthdate, testUser.Email,
            testUser.PhoneNumber, testUser.VehicleType, testUser.LicenseNumber);

        Assert.That(result.Name, Is.EqualTo(testUser.Name));
        Assert.That(result.Surname, Is.EqualTo(testUser.Surname));
        Assert.That(result.Birthdate, Is.EqualTo(testUser.Birthdate));
        Assert.That(result.Email, Is.EqualTo(testUser.Email));
        Assert.That(result.PhoneNumber, Is.EqualTo(testUser.PhoneNumber));
        Assert.That(result.LicenseNumber, Is.EqualTo(testUser.LicenseNumber));
        Assert.That(result.Trips, Is.EqualTo(testUser.Trips));
    }

    [Test]
    public void UserPhoneNumber_RightPatternWith86_ShouldSucceed()
    {
        _user.PhoneNumber = "868686868";

        var result = Regex.IsMatch(_user.PhoneNumber, _phoneNumberPattern);

        Assert.IsTrue(result);
    }

    [Test]
    public void UserPhoneNumber_WrongPatternWith86_ShouldFail()
    {
        _user.PhoneNumber = "8686868682";

        var result = Regex.IsMatch(_user.PhoneNumber, _phoneNumberPattern);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserPhoneNumber_WrongPatternWith370_ShouldFail()
    {
        _user.PhoneNumber = "37066868686";

        var result = Regex.IsMatch(_user.PhoneNumber, _phoneNumberPattern);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserPhoneNumber_RightPatternWith370_ShouldSucceed()
    {
        _user.PhoneNumber = "+37066868686";

        var result = Regex.IsMatch(_user.PhoneNumber, _phoneNumberPattern);

        Assert.IsTrue(result);
    }

    [Test]
    public void UserEmail_WrongPatternNotMachingLastString_ShouldFail()
    {
        _user.Email = "name@gmail.something";

        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserEmail_RightPattern_ShouldSucceed()
    {
        _user.Email = "name@gmail.com";
        
        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.IsTrue(result);
    }

    [Test]
    public void UserEmail_WrongPatternNoAtSign_ShouldFail()
    {
        _user.Email = "namegmail.com";

        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserEmail_WrongPatternNotMachingLength_ShouldFail()
    {
        _user.Email = new string('a', 257) + "@gmail.com";

        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserEmail_WrongPatternNotMachingFormat_ShouldFail()
    {
        _user.Email = "namegmail.something";

        var result = Regex.IsMatch(_user.Email, _emailPattern);

        Assert.IsFalse(result);
    }


    [Test]
    public void UserLicenseNumber_RightPattern_ShouldSucceed()
    {
        _user.LicenseNumber = "AAA111";

        var result = Regex.IsMatch(_user.LicenseNumber, _licenseNumberPattern);

        Assert.IsTrue(result);
    }

    [Test]
    public void UserLicenseNumber_WrongPatternNotMatchingLength_ShouldSucceed()
    {
        _user.LicenseNumber = "AAA1112";

        var result = Regex.IsMatch(_user.LicenseNumber, _licenseNumberPattern);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserLicenseNumber_WrongPatternLetterAndNumbersSwitched_ShouldSucceed()
    {
        _user.LicenseNumber = "111AAA";

        var result = Regex.IsMatch(_user.LicenseNumber, _licenseNumberPattern);

        Assert.IsFalse(result);
    }
}