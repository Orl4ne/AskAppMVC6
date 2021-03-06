﻿using AskApp.Ask.DAL.Repositories;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AskApp.Ask.DAL.Tests.QuestionRepoTests
{
    [TestClass]
    public class ModifyQuestionTests
    {
        [TestMethod]
        public void ModifyQuestion_Successful()
        {

            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            //ACT
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var result = questionRepository.Create(question);
            context.SaveChanges();
            //ACT
            result.IsResolved = true;
            var test = questionRepository.Modify(result);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual(true, test.IsResolved);

        }

        [TestMethod]
        public void ModifyQuestion_ProvidingNonExistingQuestion_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            
            Assert.ThrowsException<ArgumentException>(() => questionRepository.Modify(question));
        }

        [TestMethod]
        public void ModifyQuestion_ProvidingNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            Assert.ThrowsException<ArgumentNullException>(() => questionRepository.Modify(null));
        }

        [TestMethod]
        public void ModifyQuestion_ProvidingNonExistingId_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);

            DateTime date = DateTime.Now;
            var question = new QuestionTO {Id=16783, IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            Assert.ThrowsException<KeyNotFoundException>(() => questionRepository.Modify(question));
        }
    }
}
