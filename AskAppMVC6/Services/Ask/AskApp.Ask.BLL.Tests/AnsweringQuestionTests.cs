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
    public class AnsweringQuestionTests
    {
        public AnswerTO MockAnswer()
        {
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            DateTime date = DateTime.Now;
            var question = new QuestionTO { Id = 1, IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, Author = user };
            
            return new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", Author = user2, AssociatedQuestion = question, };
        }
        public QuestionTO MockQuestion()
        {
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, Author = user };

            return question;
        }
        [TestMethod]
        public void AnsweringQuestion_Successful()
        {
            var mockAnswerRepository = new Mock<IAnswerRepository>();
            mockAnswerRepository.Setup(u => u.Create(It.IsAny<AnswerTO>()))
                          .Returns(MockAnswer);
            var mockQuestionRepository = new Mock<IQuestionRepository>();
            mockQuestionRepository.Setup(u => u.GetById(It.IsAny<int>()))
                          .Returns(MockQuestion);
            var mockAskUserRepository = new Mock<IAskUserRepository>();

            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            DateTime date = DateTime.Now;
            var question = new QuestionTO {IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, Author = user };
            var answer2 =  new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", Author = user2, AssociatedQuestion = question, };

            var askUC = new AskUC(mockAnswerRepository.Object, mockQuestionRepository.Object, mockAskUserRepository.Object);
            var addedAnswer = askUC.AnsweringQuestion(1, answer2);

            Assert.IsNotNull(addedAnswer);
            mockAnswerRepository.Verify((m => m.Create(It.IsAny<AnswerTO>())), Times.Once());
        }
        [TestMethod]
        public void AnsweringQuestion_NulAnswerSubmitted_ThrowException()
        {
            var mockAnswerRepository = new Mock<IAnswerRepository>();
            var mockAskUserRepository = new Mock<IAskUserRepository>();
            var mockQuestionRepository = new Mock<IQuestionRepository>();
            var askUC = new AskUC(mockAnswerRepository.Object, mockQuestionRepository.Object, mockAskUserRepository.Object);

            Assert.ThrowsException<NullReferenceException>(() => askUC.AnsweringQuestion(1,null));
        }
    }
}
