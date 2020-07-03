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
    public class CreateAskUserTests
    {
        [TestMethod]
        public void CreateMedia_Successful()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            //ACT
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var result = askUserRepository.Create(user);
            context.SaveChanges();

            //ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Jean-Claude", result.FirstName);
            Assert.AreEqual("DuPet", result.LastName);
        }

        [TestMethod]
        public void CreateMedia_AddNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            Assert.ThrowsException<ArgumentNullException>(() => askUserRepository.Create(null));
        }

        [TestMethod]
        public void CreateMedia_AddExistingMedia_DoNotInsertTwiceInDb()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder<AskContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var result = askUserRepository.Create(user);
            context.SaveChanges();
            //ACT
            askUserRepository.Create(result);
            context.SaveChanges();
            
            //ASSERT
            Assert.AreEqual(1, context.AskUsers.Count());
        }
    }
}
