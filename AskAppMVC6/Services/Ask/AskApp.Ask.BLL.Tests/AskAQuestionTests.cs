﻿using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.BLL.Tests
{
    [TestClass]
    public class AskAQuestionTests
    {
        public QuestionTO MockQuestion()
        {
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, AuthorId = 1 };

            return question;
        }
        [TestMethod]
        public void AskAQuestion_Successful()
        {
            var mockQuestionRepository = new Mock<IQuestionRepository>();
            mockQuestionRepository.Setup(u => u.Create(It.IsAny<QuestionTO>()))
                         .Returns(MockQuestion);
            var mockAnswerRepository = new Mock<IAnswerRepository>();

            DateTime date = DateTime.Now;
            var question2 = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, AuthorId = 1 };

            var askUC = new AskUC(mockAnswerRepository.Object, mockQuestionRepository.Object);
            var addedQuestion = askUC.AskAQuestion(question2);

            Assert.IsNotNull(addedQuestion);
            mockQuestionRepository.Verify((m => m.Create(It.IsAny<QuestionTO>())), Times.Once());
        }
        [TestMethod]
        public void AskAQuestion_NullQuestionSubmitted_ThrowException()
        {
            var mockQuestionRepository = new Mock<IQuestionRepository>();
            var mockAnswerRepository = new Mock<IAnswerRepository>();

            var askUC = new AskUC(mockAnswerRepository.Object, mockQuestionRepository.Object);

            Assert.ThrowsException<NullReferenceException>(() => askUC.AskAQuestion(null));
        }
    }
}
