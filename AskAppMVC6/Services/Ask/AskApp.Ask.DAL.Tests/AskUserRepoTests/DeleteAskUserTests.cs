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
    public class DeleteAskUserTests
    {
        [TestMethod]
        public void DeleteAskUser_Successful()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            //ARRANGE
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            var user3 = new AskUserTO { FirstName = "Steve", LastName = "Pantelon" };
            askUserRepository.Create(user);
            var result = askUserRepository.Create(user2);
            askUserRepository.Create(user3);
            context.SaveChanges();

            //ACT
            var test = askUserRepository.Delete(result);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual(2, context.AskUsers.Count());
        }

        [TestMethod]
        public void DeleteAskUser_ProvidingNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            Assert.ThrowsException<KeyNotFoundException>(() => askUserRepository.Delete(null));
        }

        [TestMethod]
        public void DeleteAskUser_ProvidingNonExistingAskUser_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };

            Assert.ThrowsException<ArgumentException>(() => askUserRepository.Delete(user));
        }
    }
}
