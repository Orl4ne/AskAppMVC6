﻿using AskApp.Ask.DAL.Repositories;
using AskApp.Common.Interfaces;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.Ask.BLL.Tests
{
    [TestClass]
    public class UCShowAllQuestionsTests
    {
        public List<QuestionTO> MockListOfQuestions()
        {
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var question2 = new QuestionTO { IsResolved = false, Message = "Comment créer un projet MVC 6?", Title = "MVC6", Date = date, AuthorId = 2 };
            var question3 = new QuestionTO { IsResolved = false, Message = "Comment faire boucle foreach?", Title = "foreach", Date = date, AuthorId = 2 };

            return new List<QuestionTO> { question, question2, question3 };
        }

        [TestMethod]
        public void ShowAllQuestions_Successful()
        {
            var mockQuestionRepository = new Mock<IQuestionRepository>();
            mockQuestionRepository.Setup(x => x.GetAll())
                .Returns(MockListOfQuestions());
            var mockAnswerRepository = new Mock<IAnswerRepository>();

            var askUC = new AskUC(mockAnswerRepository.Object, mockQuestionRepository.Object);
            var questions = askUC.ShowAllQuestions();

            Assert.AreEqual(3, questions.Count());
            mockQuestionRepository.Verify((m => m.GetAll()), Times.Once());
        }

    }
}
