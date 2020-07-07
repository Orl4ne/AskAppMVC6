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
    public class AnswerExtensionsTests
    {
        [TestMethod]
        public void ToTransfertObject_Successful()
        {
            //ARRANGE
            DateTime date = DateTime.Now;
            var question = new QuestionEF { Id = 1, IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var answer = new AnswerEF { Message = "En fait, c'est facile il faut toujorus faire des tests", AuthorId = 2, AssociatedQuestion=question, };
            //ACT
            var result = answer.ToTransferObject();
            //Assert
            Assert.AreEqual(answer.AuthorId, result.AuthorId);
            Assert.AreEqual(answer.Message, result.Message);
        }

        [TestMethod]
        public void ToTransfertObject_ProvidingNull_ThrowException()
        {
            //ARRANGE
            AnswerEF answer = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => answer.ToTransferObject());
        }
        [TestMethod]
        public void ToEF_Successful()
        {
            //ARRANGE
            DateTime date = DateTime.Now;
            var question = new QuestionTO { Id = 1, IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var answer = new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", AuthorId = 2, AssociatedQuestion = question, };
            //ACT
            var result = answer.ToEF();
            //Assert
            Assert.AreEqual(answer.AuthorId, result.AuthorId);
            Assert.AreEqual(answer.Message, result.Message);
        }

        [TestMethod]
        public void ToTEF_ProvidingNull_ThrowException()
        {
            //ARRANGE
            AnswerTO answer = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => answer.ToEF());
        }

        [TestMethod]
        public void ToTrackedEF_ProvidingNullEF_ThrowException()
        {
            //ARRANGE
            AnswerTO answer = null;
            AnswerEF answerToModify = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => answer.ToTrackedEF(answerToModify));
        }

        [TestMethod]
        public void ToTrackedEF_ProvidingNullTO_ThrowException()
        {
            //ARRANGE
            AnswerTO answer = null;
            DateTime date = DateTime.Now;
            var question = new QuestionEF { Id = 1, IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var answerToModify = new AnswerEF { Message = "En fait, c'est facile il faut toujorus faire des tests", AuthorId = 2, AssociatedQuestion = question, };
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => answer.ToTrackedEF(answerToModify));
        }
    }
}
