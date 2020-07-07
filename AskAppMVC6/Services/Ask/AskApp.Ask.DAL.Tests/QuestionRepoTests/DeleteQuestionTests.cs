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
        public void DeleteQuestion_Successful()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var question2 = new QuestionTO { IsResolved = false, Message = "Comment créer un projet MVC 6", Title = "MVC6", Date = date, AuthorId = 2 };
            var question3 = new QuestionTO { IsResolved = false, Message = "Comment faire boucle foreach", Title = "foreach", Date = date, AuthorId = 2 };
            var addedQuestion = questionRepository.Create(question);
            var addedQuestion2 = questionRepository.Create(question2);
            var addedQuestion3 = questionRepository.Create(question3);
            context.SaveChanges();

            //ACT
            var test = questionRepository.Delete(addedQuestion3);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual(2, context.Questions.Count());
        }

        [TestMethod]
        public void DeleteQuestion_ProvidingNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            Assert.ThrowsException<KeyNotFoundException>(() => questionRepository.Delete(null));
        }

        [TestMethod]
        public void DeleteQuestion_ProvidingNonExistingQuestion_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };

            Assert.ThrowsException<ArgumentException>(() => questionRepository.Delete(question));
        }
    }
}
