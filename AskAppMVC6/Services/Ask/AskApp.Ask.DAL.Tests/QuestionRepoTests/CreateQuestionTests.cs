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
    public class CreateQuestionTests
    {
        [TestMethod]
        public void CreateQuestion_Successful()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            //ACT
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var result = questionRepository.Create(question);
            //ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("Je n'arrive pas à faire un test", result.Message);
            Assert.AreEqual("Problème avec Tests", result.Title);
            Assert.AreEqual(date, result.Date);
            Assert.AreEqual(false, result.IsArchived);
            Assert.AreEqual(1, result.AuthorId);
        }

        [TestMethod]
        public void CreateQuestion_AddNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            Assert.ThrowsException<ArgumentNullException>(() => questionRepository.Create(null));
        }

        [TestMethod]
        public void CreateQuestion_AddExistingQuestion_DoNotInsertTwiceInDb()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder<AskContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var result = questionRepository.Create(question);
            context.SaveChanges();
            //ACT
            questionRepository.Create(result);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual(1, context.Questions.Count());
        }
    }
}
