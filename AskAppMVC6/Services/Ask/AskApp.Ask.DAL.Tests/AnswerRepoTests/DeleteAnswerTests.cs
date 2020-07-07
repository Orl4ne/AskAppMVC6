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

namespace AskApp.Ask.DAL.Tests.AnswerRepoTests
{
    [TestClass]
    public class DeleteAnswerTests
    {
        [TestMethod]
        public void DeleteAnswer_Successful()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            //ACT
            
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var question2 = new QuestionTO { IsArchived = false, Message = "Comment créer un projet MVC 6", Title = "MVC6", Date = date, AuthorId = 2 };
            var question3 = new QuestionTO { IsArchived = false, Message = "Comment faire boucle foreach", Title = "foreach", Date = date, AuthorId = 2 };
            var addedQuestion = questionRepository.Create(question);
            var addedQuestion2 = questionRepository.Create(question2);
            var addedQuestion3 = questionRepository.Create(question3);
            context.SaveChanges();
            var answer = new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", AuthorId = 2, AssociatedQuestion = addedQuestion, };
            var answer2 = new AnswerTO { Message = "Oui c'est trop facile les tests avec InMemory", AuthorId = 3, AssociatedQuestion = addedQuestion, };
            var answer3 = new AnswerTO { Message = "Tu dois d'abord créer un projet web app avec .Net Core", AuthorId = 1, AssociatedQuestion = addedQuestion2, };
            var addedAnswer = answerRepository.Create(answer);
            var addedAnswer2 = answerRepository.Create(answer2);
            var addedAnswer3 = answerRepository.Create(answer3);
            context.SaveChanges();

            //ACT
            var test = answerRepository.Delete(addedAnswer3);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual(2, context.Answers.Count());
        }

        [TestMethod]
        public void DeleteAnswer_ProvidingNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new AskContext(options);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            Assert.ThrowsException<KeyNotFoundException>(() => answerRepository.Delete(null));
        }

        [TestMethod]
        public void DeleteAnswer_ProvidingNonExistingAnswer_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new AskContext(options);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            //ACT
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, AuthorId = 1 };
            var addedQuestion = questionRepository.Create(question);
            context.SaveChanges();
            var answer = new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", AuthorId = 2, AssociatedQuestion = addedQuestion, };

            Assert.ThrowsException<ArgumentException>(() => answerRepository.Delete(answer));
        }
    }
}
