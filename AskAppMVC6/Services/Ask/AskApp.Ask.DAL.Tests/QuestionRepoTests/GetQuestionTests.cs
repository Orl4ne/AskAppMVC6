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
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            var addedUser = askUserRepository.Create(user);
            var addedUser2 = askUserRepository.Create(user2);
            context.SaveChanges();
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, Author = addedUser };
            var question2 = new QuestionTO { IsArchived = false, Message = "Comment créer un projet MVC 6?", Title = "MVC6", Date = date, Author = addedUser2 };
            var question3 = new QuestionTO { IsArchived = false, Message = "Comment faire boucle foreach?", Title = "foreach", Date = date, Author = addedUser2 };
            var addedQuestion = questionRepository.Create(question);
            var addedQuestion2 = questionRepository.Create(question2);
            var addedQuestion3 = questionRepository.Create(question3);
            context.SaveChanges();

            //ACT
            var test = questionRepository.GetAll();

            //ASSERT
            Assert.AreEqual(3, test.Count());
        }

        [TestMethod]
        public void GetAllQuestions_NoQuestionInDb_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
              .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
              .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
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
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            //ARRANGE
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            var addedUser = askUserRepository.Create(user);
            var addedUser2 = askUserRepository.Create(user2);
            context.SaveChanges();
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test!", Title = "Problème avec Tests", Date = date, Author = addedUser };
            var question2 = new QuestionTO { IsArchived = false, Message = "Comment créer un projet MVC 6?", Title = "MVC6", Date = date, Author = addedUser2 };
            var question3 = new QuestionTO { IsArchived = false, Message = "Comment faire boucle foreach?", Title = "foreach", Date = date, Author = addedUser2 };
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
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            //ARRANGE
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var addedUser = askUserRepository.Create(user);
            context.SaveChanges();
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, Author = addedUser };

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
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            //ASSERT
            Assert.ThrowsException<ArgumentNullException>(() => questionRepository.GetById(14));
        }
    }
}
