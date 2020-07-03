using AskApp.Ask.DAL.Repositories;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AskApp.Ask.DAL.Tests.AskUserRepoTests
{
    [TestClass]
    public class ModifyAskUserTests
    {
        [TestMethod]
        public void ModifyAskUser_Successful()
        {

            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            //ARRANGE
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var result = askUserRepository.Create(user);
            context.SaveChanges();

            //ACT
            result.FirstName = "Stève";
            var test = askUserRepository.Modify(result);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual("Stève", test.FirstName);

        }

        [TestMethod]
        public void ModifyAskUser_ProvidingNonExistingAskUser_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            Assert.ThrowsException<ArgumentException>(() => askUserRepository.Modify(user));
        }

        [TestMethod]
        public void ModifyAskUser_ProvidingNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            Assert.ThrowsException<ArgumentNullException>(() => askUserRepository.Modify(null));
        }

        [TestMethod]
        public void ModifyAskUser_ProvidingNonExistingId_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);

            var user = new AskUserTO {Id= 16782, FirstName = "Jean-Claude", LastName = "DuPet" };
            Assert.ThrowsException<KeyNotFoundException>(() => askUserRepository.Modify(user));
        }
    }
}
