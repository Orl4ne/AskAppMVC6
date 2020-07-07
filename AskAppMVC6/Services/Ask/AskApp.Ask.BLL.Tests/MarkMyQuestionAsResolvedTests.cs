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
    public class MarkMyQuestionAsResolvedTests
    {
        public QuestionTO MockQuestion()
        {
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, AuthorId = 1 };

            return question;
        }

        [TestMethod]
        public void MarkMyQuestionAsResolved_Successful()
        {
            var mockQuestionRepository = new Mock<IQuestionRepository>();
            mockQuestionRepository.Setup(u => u.Modify(It.IsAny<QuestionTO>()))
                .Returns(MockQuestion);
            mockQuestionRepository.Setup(u => u.GetById(It.IsAny<int>()))
                .Returns(MockQuestion);
            var mockAnswerRepository = new Mock<IAnswerRepository>();

            var askUC = new AskUC(mockAnswerRepository.Object, mockQuestionRepository.Object);
            var modifiedQuestion = askUC.MarkMyQuestionAsResolved(1);

            Assert.IsNotNull(modifiedQuestion);
            mockQuestionRepository.Verify((m => m.GetById(It.IsAny<int>())), Times.Once());
            mockQuestionRepository.Verify((m => m.Modify(It.IsAny<QuestionTO>())), Times.Once());
        }

        [TestMethod]
        public void MarkMyQuestionAsResolved_NonExistingQuestionSubmitted_ThrowException()
        {
            var mockAnswerRepository = new Mock<IAnswerRepository>();
            var mockQuestionRepository = new Mock<IQuestionRepository>();
            var askUC = new AskUC(mockAnswerRepository.Object, mockQuestionRepository.Object);

            Assert.ThrowsException<NullReferenceException>(() => askUC.MarkMyQuestionAsResolved(1678));
        }
    }
}
