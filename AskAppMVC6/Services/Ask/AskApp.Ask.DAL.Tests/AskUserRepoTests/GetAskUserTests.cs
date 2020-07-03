using AskApp.Ask.DAL.Repositories;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AskApp.Ask.DAL.Tests.AskUserRepoTests
{
    [TestClass]
    public class GetAskUserTests
    {
        [TestMethod]
        public void GetALlAskUsers_Successful()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            //ARRANGE
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            var user3 = new AskUserTO { FirstName = "Sylvanus", LastName = "Lepélé" };
            askUserRepository.Create(user);
            askUserRepository.Create(user2);
            askUserRepository.Create(user3);
            context.SaveChanges();

            //ACT
            var test = askUserRepository.GetAll();

            //ASSERT
            Assert.AreEqual(3, test.Count());
        }

        [TestMethod]
        public void GetAllAskUsers_NoMediaInDb_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            Assert.ThrowsException<ArgumentNullException>(() => askUserRepository.GetAll());
        }

        [TestMethod]
        public void GetAskUserById_Successful()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            //ARRANGE
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            var user3 = new AskUserTO { FirstName = "Sylvanus", LastName = "Lepélé" };
            askUserRepository.Create(user);
            askUserRepository.Create(user2);
            askUserRepository.Create(user3);
            context.SaveChanges();

            //ACT
            var test = askUserRepository.GetById(2);

            //ASSERT
            Assert.AreEqual("Martine", test.FirstName);

        }

        [TestMethod]
        public void GetAskUserById_ProvidingNoId_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            //ARRANGE
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };

            //ASSERT
            Assert.ThrowsException<ArgumentException>(() => askUserRepository.GetById(user.Id));
        }

        [TestMethod]
        public void GetAskUserById_ProvidingNonExistingId_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => askUserRepository.GetById(14));
        }
    }
}
