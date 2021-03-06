﻿using AskApp.Ask.DAL.Repositories;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AskApp.Ask.DAL.Tests.AnswerRepoTests
{
    [TestClass]
    public class CreateAnswerTests
    {
        [TestMethod]
        public void CreateAnswer_Successful()
        {
            // ARRANGE
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            //ACT
            
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var addedQuestion = questionRepository.Create(question);
            context.SaveChanges();
            var answer = new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", AuthorId = 2, AssociatedQuestion = addedQuestion, };
            var result = answerRepository.Create(answer);
            context.SaveChanges();

            //ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("En fait, c'est facile il faut toujorus faire des tests", result.Message);
            Assert.AreEqual(2, result.AuthorId);
        }

        [TestMethod]
        public void CreateAnswer_AddNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAnswerRepository answerRepository = new AnswerRepository(context);
            Assert.ThrowsException<ArgumentNullException>(() => answerRepository.Create(null));
        }

        [TestMethod]
        public void CreateAnswer_AddExistingAnswer_DoNotInsertTwiceInDb()
        {
            //ARRANGE
            var options = new DbContextOptionsBuilder<AskContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsResolved = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var addedQuestion = questionRepository.Create(question);
            context.SaveChanges();
            var answer = new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", AuthorId = 2, AssociatedQuestion = addedQuestion, };
            var result = answerRepository.Create(answer);
            context.SaveChanges();
            //ACT
            answerRepository.Create(result);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual(1, context.Answers.Count());
        }
    }
}
