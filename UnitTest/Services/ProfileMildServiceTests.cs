using BehaviourManagementSystem_API.Data.EF;
using BehaviourManagementSystem_API.Models;
using BehaviourManagementSystem_API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.Services
{
    public class ProfileServiceTests
    {
        ProfileMildService profilemidservice;
        ApplicationDbContext _context;
        [SetUp]
        public void Setup()
        {
            profilemidservice = new ProfileMildService(_context);
        }

        [Test]
        public void GetAll_Works()
        {
            //Arrange
            var profilemild = new List<ProfileMild>() { new ProfileMild { Id = new Guid("223f031d-3d82-48de-877a-1281024f99c2"), Content = "abc" },
            new ProfileMild { Id = new Guid("223f031d-3d82-48de-877a-1281024f99c3"), Content = "abc3" },
            new ProfileMild { Id = new Guid("223f031d-3d82-48de-877a-1281024f99c4"), Content = "abc4" },
            new ProfileMild { Id = new Guid("223f031d-3d82-48de-877a-1281024f99c5"), Content = "abc5" },};
            //Act
            var GetAll = profilemidservice.GetAll();
            //Assert
            Assert.That(GetAll, Is.Not.Null);
        }

        [Test]
        public void GetbyId_Works()
        {
            //Arrange
            string id = "223f031d-3d82-48de-877a-1281024f99c2";
            var profilemild = new List<ProfileMild>() { new ProfileMild { Id = new Guid("223f031d-3d82-48de-877a-1281024f99c2"), Content = "abc" },
            new ProfileMild { Id = new Guid("223f031d-3d82-48de-877a-1281024f99c3"), Content = "abc3" },
            new ProfileMild { Id = new Guid("223f031d-3d82-48de-877a-1281024f99c4"), Content = "abc4" },
            new ProfileMild { Id = new Guid("223f031d-3d82-48de-877a-1281024f99c5"), Content = "abc5" },};
            //Act
            var GetbyId = profilemidservice.GetById(id);
            //Assert
            Assert.That(GetbyId, Is.EqualTo(profilemild.Where(prop => prop.Id.ToString() == id)));
        }
    }
}