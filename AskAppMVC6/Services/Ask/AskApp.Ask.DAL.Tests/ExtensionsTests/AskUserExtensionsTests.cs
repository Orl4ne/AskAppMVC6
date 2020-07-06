using AskApp.Ask.DAL.Entities;
using AskApp.Ask.DAL.Extensions;
using AskApp.Common.TOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.DAL.Tests.ExtensionsTests
{
    [TestClass]
    public class AskUserExtensionsTests
    {
        [TestMethod]
        public void ToTransfertObject_Successful()
        {
            //ARRANGE
            DateTime date = DateTime.Now;
            var user = new AskUserEF { Id = 4, FirstName = "Jean-Claude", LastName = "DuPet" };
            //ACT
            var result = user.ToTransferObject();
            //Assert
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
        }

        [TestMethod]
        public void ToTransfertObject_ProvidingNull_ThrowException()
        {
            //ARRANGE
            AskUserEF user = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => user.ToTransferObject());
        }
        [TestMethod]
        public void ToEF_Successful()
        {
            //ARRANGE
            DateTime date = DateTime.Now;
            var user = new AskUserTO { Id = 4, FirstName = "Jean-Claude", LastName = "DuPet" };
            //ACT
            var result = user.ToEF();
            //Assert
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
        }

        [TestMethod]
        public void ToTEF_ProvidingNull_ThrowException()
        {
            //ARRANGE
            AskUserTO user = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => user.ToEF());
        }

        [TestMethod]
        public void ToTrackedEF_ProvidingNullEF_ThrowException()
        {
            //ARRANGE
            AskUserTO user = null;
            AskUserEF userToModify = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => user.ToTrackedEF(userToModify));
        }
        [TestMethod]
        public void ToTrackedEF_ProvidingNullTO_ThrowException()
        {
            //ARRANGE
            AskUserTO user = null;
            DateTime date = DateTime.Now;
            var userToModify = new AskUserEF { Id = 4, FirstName = "Jean-Claude", LastName = "DuPet" };
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => user.ToTrackedEF(userToModify));
        }
    }
}
