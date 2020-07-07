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

namespace AskApp.Ask.DAL.Tests.QuestionRepoTests
{
    [TestClass]
    public class GetQuestionTests
    {
        [TestMethod]
        public void GetALlQuestions_Successful()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder<AskContext>()
               .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
               .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var question2 = new QuestionTO { IsArchived = true, Message = "Comment créer un projet MVC 6?", Title = "MVC6", Date = date, AuthorId = 2 };
            var question3 = new QuestionTO { IsArchived = false, Message = "Comment faire boucle foreach?", Title = "foreach", Date = date, AuthorId = 2 };
            var addedQuestion = questionRepository.Create(question);
            var addedQuestion2 = questionRepository.Create(question2);
            var addedQuestion3 = questionRepository.Create(question3);
            context.SaveChanges();

            //ACT
            var test = questionRepository.GetAll();

            //ASSERT
            Assert.AreEqual(2, test.Count()); // Should be 2 because for the 2nd question, IsArchived = true
        }

        [TestMethod]
        public void GetAllQuestions_NoQuestionInDb_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
              .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
              .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            Assert.ThrowsException<ArgumentNullException>(() => questionRepository.GetAll());
        }

        [TestMethod]
        public void GetQuestionById_Successful()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
               .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
               .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            //ARRANGE
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var question2 = new QuestionTO { IsArchived = false, Message = "Comment créer un projet MVC 6?", Title = "MVC6", Date = date, AuthorId = 2 };
            var question3 = new QuestionTO { IsArchived = false, Message = "Comment faire boucle foreach?", Title = "foreach", Date = date, AuthorId = 2 };
            var addedQuestion = questionRepository.Create(question);
            var addedQuestion2 = questionRepository.Create(question2);
            var addedQuestion3 = questionRepository.Create(question3);
            context.SaveChanges();

            //ACT
            var test =questionRepository.GetById(2);

            //ASSERT
            Assert.AreEqual("Comment créer un projet MVC 6?", test.Message);
        }

        [TestMethod]
        public void GetQuestionById_ProvidingNoId_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            //ARRANGE
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };

            //ASSERT
            Assert.ThrowsException<ArgumentException>(() => questionRepository.GetById(question.Id));
        }

        [TestMethod]
        public void GetQuestionById_ProvidingNonExistingId_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => questionRepository.GetById(14));
        }
    }
}
