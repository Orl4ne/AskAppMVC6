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
    public class DeleteQuestionTests
    {
        [TestMethod]
        public void DeleteAskUser_Successful()
        {
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
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, Author = addedUser };
            var question2 = new QuestionTO { IsArchived = false, Message = "Comment créer un projet MVC 6", Title = "MVC6", Date = date, Author = addedUser2 };
            var question3 = new QuestionTO { IsArchived = false, Message = "Comment faire boucle foreach", Title = "foreach", Date = date, Author = addedUser2 };
            var addedQuestion = questionRepository.Create(question);
            var addedQuestion2 = questionRepository.Create(question2);
            var addedQuestion3 = questionRepository.Create(question3);
            context.SaveChanges();

            //ACT
            var test = questionRepository.Delete(addedQuestion3);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual(2, context.AskUsers.Count());
        }

        [TestMethod]
        public void DeleteAskUser_ProvidingNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            Assert.ThrowsException<KeyNotFoundException>(() => questionRepository.Delete(null));
        }

        [TestMethod]
        public void DeleteAskUser_ProvidingNonExistingAskUser_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var addedUser = askUserRepository.Create(user);
            context.SaveChanges();
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, Author = addedUser };

            Assert.ThrowsException<ArgumentException>(() => questionRepository.Delete(question));
        }
    }
}
