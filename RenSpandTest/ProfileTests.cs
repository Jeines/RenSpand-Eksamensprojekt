using RenspandWebsite.Service.ProfileServices;
using RenspandWebsite.Exceptions;
using RenspandWebsite.Models;
using RenspandWebsite.Pages.Profiles;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenspandWebsite.Service.UnitTestClasses;

namespace RenSpandTest
{
    [TestClass]
    public class CreateModelTests
    {
        private UnitTestProfileCreate _profileService;

        [TestInitialize]
        public void Setup()
        {
            _profileService = new UnitTestProfileCreate();
        }

        [TestMethod]
        public void ValidInput()
        {
            // Arrange
            var profile = new Profile
            {
                Username = "validUser",
                Email = "valid@example.com",
                Password = "strongpassword"
            };

            // Act
            _profileService.AddProfile(profile);

            // Assert
            Assert.AreEqual(1, _profileService.Profiles.Count);
            Assert.AreEqual("validUser", _profileService.Profiles[0].Username);
            Assert.AreEqual("valid@example.com", _profileService.Profiles[0].Email);
        }

        [TestMethod]
        public void InvalidUsername()
        {
            // Arrange
            Profile profile = new Profile
            {
                Username = "short",
                Email = "valid@example.com",
                Password = "strongpassword"
            };

            // Assert & Act
            Assert.ThrowsException<InvalidUsernameException>(() => _profileService.AddProfile(profile));
        }

        [TestMethod]
        public void InvalidPassword()
        {
            // Arrange
            Profile profile = new Profile
            {
                Username = "validuser",
                Email = "valid@example.com",
                Password = "123"
            };
            // Assert & Act
            Assert.ThrowsException<InvalidPasswordException>(() => _profileService.AddProfile(profile));
        }


        [TestMethod]
        public void InvalidEmail()
        {
            // Arrange
            Profile profile = new Profile
            {
                Username = "validuser",
                Email = "invalidemail",
                Password = "strongpassword"
            };

            // Assert & Act
            Assert.ThrowsException<InvalidEmailException>(() => _profileService.AddProfile(profile));
        }
    }
}