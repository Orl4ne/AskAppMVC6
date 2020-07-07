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
    public class QuestionExtensionsTests
    {
        [TestMethod]
        public void ToTransfertObject_Successful()
        {
            //ARRANGE
            DateTime date = DateTime.Now;
            var question = new QuestionEF { Id = 1, IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId=1 };
            //ACT
            var result = question.ToTransferObject();
            //Assert
            Assert.AreEqual(question.Id, result.Id);
            Assert.AreEqual(question.IsResolved, result.IsResolved);
            Assert.AreEqual(question.AuthorId, result.AuthorId);
            Assert.AreEqual(question.Title, result.Title);
            Assert.AreEqual(question.Date, result.Date);
            Assert.AreEqual(question.Message, result.Message);
        }

        [TestMethod]
        public void ToTransfertObject_ProvidingNull_ThrowException()
        {
            //ARRANGE
            QuestionEF question = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => question.ToTransferObject());
        }
        [TestMethod]
        public void ToEF_Successful()
        {
            //ARRANGE
            DateTime date = DateTime.Now;
            var question = new QuestionTO { Id = 1, IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            //ACT
            var result = question.ToEF();
            //Assert
            Assert.AreEqual(question.Id, result.Id);
            Assert.AreEqual(question.IsResolved, result.IsResolved);
            Assert.AreEqual(question.AuthorId, result.AuthorId);
            Assert.AreEqual(question.Title, result.Title);
            Assert.AreEqual(question.Date, result.Date);
            Assert.AreEqual(question.Message, result.Message);
        }

        [TestMethod]
        public void ToTEF_ProvidingNull_ThrowException()
        {
            //ARRANGE
            QuestionTO question = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => question.ToEF());
        }

        [TestMethod]
        public void ToTrackedEF_ProvidingNullEF_ThrowException()
        {
            //ARRANGE
            QuestionTO question = null;
            QuestionEF questionToModify = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => question.ToTrackedEF(questionToModify));
        }
        [TestMethod]
        public void ToTrackedEF_ProvidingNullTO_ThrowException()
        {
            //ARRANGE
            QuestionTO question = null;
            DateTime date = DateTime.Now;
            var questionToModify = new QuestionEF { Id = 1, IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => question.ToTrackedEF(questionToModify));
        }
    }
}
