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
    /// <summary>
    /// Testklasse for at oprette profiler i RenSpand-appen.
    /// </summary>
    [TestClass]
    public class CreateModelTests
    {
        // Servicen for at oprette profiler
        private UnitTestProfileCreate _profileService;

        // Test initialisering, der køres før hver test
        [TestInitialize]
        public void Setup()
        {
            // Initialiserer en ny instans af UnitTestProfileCreate for hver test methode
            _profileService = new UnitTestProfileCreate();
        }

        /// <summary>
        /// tester en gyldig profiloprettelse.
        /// </summary>
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
            // Tilføjer profilen til service
            _profileService.AddProfile(profile);

            // Assert
            // Kontrollerer at profilen er blevet tilføjet korrekt
            Assert.AreEqual(1, _profileService.Profiles.Count);
            Assert.AreEqual("validUser", _profileService.Profiles[0].Username);
            Assert.AreEqual("valid@example.com", _profileService.Profiles[0].Email);
        }

        /// <summary>
        /// Tester en ugyldig brugernavn, der er for kort.
        /// </summary>
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

        /// <summary>
        /// Tester en ugyldig adgangskode, der er for kort.
        /// </summary>
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

        /// <summary>
        /// Tester en ugyldig e-mail, der ikke er i korrekt format.
        /// </summary>
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