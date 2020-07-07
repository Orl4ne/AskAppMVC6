using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.BLL.Tests
{
    [TestClass]
    public class ShowThisQuestionTests
    {
        public QuestionTO MockQuestion()
        {
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, AuthorId = 1 };

            return question;
        }
        [TestMethod]
        public void ShowThisQuestion_Successful()
        {
            var mockQuestionRepository = new Mock<IQuestionRepository>();
            mockQuestionRepository.Setup(u => u.GetById(It.IsAny<int>()))
                          .Returns(MockQuestion);
            var mockAnswerRepository = new Mock<IAnswerRepository>();

            var askUC = new AskUC(mockAnswerRepository.Object, mockQuestionRepository.Object);
            var questionToShow = askUC.ShowThisQuestion(1);
            
            Assert.IsNotNull(questionToShow);
            mockQuestionRepository.Verify((m => m.GetById(It.IsAny<int>())), Times.Once());
        }
    }
}
