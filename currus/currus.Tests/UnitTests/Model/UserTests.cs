using currus.Enums;
using currus.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace currus.Tests.Model;

[TestFixture]
internal class UserTests
{
    private User _user;


    [SetUp]
    public void SetUp()
    {
        _user = new User();
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
        var id = 0;
        var name = "Name";
        var surname = "Surname";
        var birthdate = new DateTime();
        var email = "name@gmail.com";
        var phoneNumber = "868686868";
        var vehicleType = "SUV";
        var licenseNumber = "AAA111";
       
        User user = new User(id, name, surname, birthdate, email, 
            phoneNumber, vehicleType, licenseNumber);

        Assert.IsNotNull(user);
    }

    [Test]
    public void UserPhoneNumber_RightPatternWith86_ShouldSucceed()
    {
        User user = new User();
        user.PhoneNumber = "868686868";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsTrue(result);
    }

    [Test]
    public void UserPhoneNumber_WrongPatternWith86_ShouldFail()
    {
        User user = new User();
        user.PhoneNumber = "8686868682";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserPhoneNumber_RightPatternWith370_ShouldSucceed()
    {
        User user = new User();
        user.PhoneNumber = "+37066868686";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsTrue(result);
    }

    [Test]
    public void UserPhoneNumber_WrongPatternWith370_ShouldFail()
    {
        User user = new User();
        user.PhoneNumber = "37066868686";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserEmail_RightPattern_ShouldSucceed()
    {
        User user = new User();
        user.Email = "name@gmail.com";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsTrue(result);
    }

    [Test]
    public void UserEmail_WrongPatternNoAtSign_ShouldFail()
    {
        User user = new User();
        user.Email = "namegmail.com";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserEmail_WrongPatternNoDot_ShouldFail()
    {
        User user = new User();
        user.Email = "name@gmailcom";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserEmail_WrongPatternNotMachingLength_ShouldFail()
    {
        User user = new User();
        user.Email = "name@gmail.something";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserLicenseNumber_RightPattern_ShouldSucceed()
    {
        User user = new User();
        user.LicenseNumber = "AAA111";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsTrue(result);
    }

    [Test]
    public void UserLicenseNumber_WrongPatternNotMatchingLength_ShouldSucceed()
    {
        User user = new User();
        user.LicenseNumber = "AAA1112";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsFalse(result);
    }

    [Test]
    public void UserLicenseNumber_WrongPatternLetterAndNumbersSwitched_ShouldSucceed()
    {
        User user = new User();
        user.LicenseNumber = "111AAA";

        var result = Validator.TryValidateObject(user, new ValidationContext(user, null, null), null, true);

        Assert.IsFalse(result);
    }
}