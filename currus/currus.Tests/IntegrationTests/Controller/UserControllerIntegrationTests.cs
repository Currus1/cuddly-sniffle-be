using currus.Controllers;
using currus.Enums;
using currus.Migrations;
using currus.Models;
using currus.Models.DTOs;
using currus.Repository;
using currus.Tests.Services;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Assert = NUnit.Framework.Assert;

namespace currus.Tests.IntegrationTests.Controller
{
    [TestFixture]
    class UserControllerIntegrationTests
    {
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var factory = new MemoryFactory();
            _client = factory.CreateClient();
        }

        private static User GetUserTestData()
        {
            var user = new User()
            {
                Name = "Testas",
                Surname = "Testavicius",
                Email = "test@email.com",
                PhoneNumber = "+37060670750",
                Birthdate = new DateTime(2022, 12, 19),
                DriversLicense = null,
                VehicleType = null,
                LicenseNumber = null,
                Trips = new List<Trip>()
            };

            return user;
        }

        private static User GetDriverTestData()
        {
            var user = new User()
            {
                Name = "Testas",
                Surname = "Testavicius",
                Email = "test@email.com",
                PhoneNumber = "+37060670750",
                Birthdate = new DateTime(2022, 12, 19),
                DriversLicense = "11114444",
                VehicleType = "SUV",
                LicenseNumber = "TKP420",
                Trips = new List<Trip>()
            };

            return user;
        }

        private static JsonContent GetRegisterTestData()
        {
            var testUser = GetUserTestData();

            var content = JsonContent.Create(new
            {
                name = testUser.Name,
                surname = testUser.Surname,
                email = testUser.Email,
                password = "Password1!",
                birthdate = testUser.Birthdate.ToShortDateString(),
                number = testUser.PhoneNumber
            });

            return content;
        }

        private static JsonContent GetLoginTestData()
        {
            var testUser = GetUserTestData();

            var content = JsonContent.Create(new
            {
                email = testUser.Email,
                password = "Password1!",
            });

            return content;
        }

        private async Task Authorize()
        {
            var registerContent = GetRegisterTestData();
            var loginContent = GetLoginTestData();

            await _client.PostAsync($"api/Auth/register", registerContent);

            if(_client.DefaultRequestHeaders.Authorization == null)
            {
                var login = await _client.PostAsync($"api/Auth/login", loginContent);
                login.EnsureSuccessStatusCode();
                var log = await login.Content.ReadAsStringAsync();

                
                var token = JObject.Parse(log)["token"];

                if(token != null)
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
                }
            }
        }

        [TestCategory("Integration")]
        [Test]
        public async Task Integration_UserController_GetUser_ReturnUserSuccess()
        {
            var testUser = GetUserTestData();

            await Authorize();

            var response = await _client.GetAsync($"apisecure/User/");
            var user = await response.Content.ReadFromJsonAsync<User>();

            response.EnsureSuccessStatusCode();
            Assert.That(response, Is.Not.Null);
            Assert.That(user, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(user.Name, Is.EqualTo(testUser.Name));
                Assert.That(user.Surname, Is.EqualTo(testUser.Surname));
                Assert.That(user.Email, Is.EqualTo(testUser.Email));
                Assert.That(user.Birthdate, Is.EqualTo(testUser.Birthdate));
                Assert.That(user.PhoneNumber, Is.EqualTo(testUser.PhoneNumber));
            });
        }

        [TestCategory("Integration")]
        [Test]
        public async Task Integration_UserController_UpdateUserAsDriver_UpdateToDriverSuccess()
        {
            var testDriver = GetDriverTestData();

            var content = JsonContent.Create(new
            {
                driversLicense = testDriver.DriversLicense,
                vehicleType = testDriver.VehicleType,
                licenseNumber = testDriver.LicenseNumber
            });

            await Authorize();

            var response1 = await _client.PutAsync($"apisecure/User/Driver", content);
            response1.EnsureSuccessStatusCode();

            var response2 = await _client.GetAsync($"apisecure/User/");
            var driver = await response2.Content.ReadFromJsonAsync<User>();

            response2.EnsureSuccessStatusCode();
            Assert.That(response2, Is.Not.Null);
            Assert.That(driver, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(driver.Name, Is.EqualTo(testDriver.Name));
                Assert.That(driver.Surname, Is.EqualTo(testDriver.Surname));
                Assert.That(driver.Email, Is.EqualTo(testDriver.Email));
                Assert.That(driver.Birthdate, Is.EqualTo(testDriver.Birthdate));
                Assert.That(driver.PhoneNumber, Is.EqualTo(testDriver.PhoneNumber));
                Assert.That(driver.DriversLicense, Is.EqualTo(testDriver.DriversLicense));
                Assert.That(driver.VehicleType, Is.EqualTo(testDriver.VehicleType));
                Assert.That(driver.LicenseNumber, Is.EqualTo(testDriver.LicenseNumber));
            });
        }

        [TestCategory("Integration")]
        [Test]
        public async Task Integration_UserController_UpdateUser_UpdateSuccess()
        {
            await Authorize();

            var testResponse = await _client.GetAsync($"apisecure/User/");
            var newUser = await testResponse.Content.ReadFromJsonAsync<User>();
            testResponse.EnsureSuccessStatusCode();

            Assert.That(newUser, Is.Not.Null);

            newUser.Name = "Teste";
            newUser.Surname = "Testaviciute";
            newUser.Email = "nottest@gmail.com";
            newUser.PhoneNumber = "867750755";
            newUser.Birthdate = new DateTime(2021, 12, 19);
            newUser.DriversLicense = "88884444";
            newUser.VehicleType = "Van";
            newUser.LicenseNumber = "BBB555";

            var content = JsonContent.Create(new
            {
                name = newUser.Name,
                surname = newUser.Surname,
                email = newUser.Email,
                phoneNumber = newUser.PhoneNumber,
                birthdate = newUser.Birthdate.ToShortDateString(),
                driversLicense = newUser.DriversLicense,
                vehicleType = newUser.VehicleType,
                licenseNumber = newUser.LicenseNumber
            });

            var response1 = await _client.PutAsync($"apisecure/User/Update", content);
            response1.EnsureSuccessStatusCode();

            var response2 = await _client.GetAsync($"apisecure/User/");
            var user = await response2.Content.ReadFromJsonAsync<User>();

            response2.EnsureSuccessStatusCode();
            Assert.That(response2, Is.Not.Null);
            Assert.That(user, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(user.Name, Is.EqualTo(newUser.Name));
                Assert.That(user.Surname, Is.EqualTo(newUser.Surname));
                Assert.That(user.Email, Is.EqualTo(newUser.Email));
                Assert.That(user.Birthdate, Is.EqualTo(newUser.Birthdate));
                Assert.That(user.PhoneNumber, Is.EqualTo(newUser.PhoneNumber));
                Assert.That(user.DriversLicense, Is.EqualTo(newUser.DriversLicense));
                Assert.That(user.VehicleType, Is.EqualTo(newUser.VehicleType));
                Assert.That(user.LicenseNumber, Is.EqualTo(newUser.LicenseNumber));
            });
        }
    }
}