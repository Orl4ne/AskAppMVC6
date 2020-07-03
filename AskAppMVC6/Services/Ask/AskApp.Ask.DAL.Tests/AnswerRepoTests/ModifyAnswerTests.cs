using AskApp.Ask.DAL.Repositories;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AskApp.Ask.DAL.Tests.AnswerRepoTests
{
    [TestClass]
    public class ModifyAnswerTests
    {
        [TestMethod]
        public void ModifyAnswer_Successful()
        {

            var options = new DbContextOptionsBuilder<AskContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            //ACT
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            var user3 = new AskUserTO { FirstName = "Steve", LastName = "Pantelon" };
            var addedUser = askUserRepository.Create(user);
            var addedUser2 = askUserRepository.Create(user2);
            var addedUser3 = askUserRepository.Create(user3);
            context.SaveChanges();
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, Author = addedUser };
            var question2 = new QuestionTO { IsArchived = false, Message = "Comment créer un projet MVC 6", Title = "MVC6", Date = date, Author = addedUser2 };
            var question3 = new QuestionTO { IsArchived = false, Message = "Comment faire boucle foreach", Title = "foreach", Date = date, Author = addedUser2 };
            var addedQuestion = questionRepository.Create(question);
            var addedQuestion2 = questionRepository.Create(question2);
            var addedQuestion3 = questionRepository.Create(question3);
            context.SaveChanges();
            var answer = new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", Author = addedUser2, AssociatedQuestion = addedQuestion, };
            var result = answerRepository.Create(answer);
            context.SaveChanges();
            //ACT
            result.Message = "Message modifié";
            var test = answerRepository.Modify(result);
            context.SaveChanges();

            //ASSERT
            Assert.AreEqual("Message modifié", test.Message);
        }

        [TestMethod]
        public void ModifyAnswer_ProvidingNonExistingAnswer_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            //ACT
            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            var addedUser = askUserRepository.Create(user);
            var addedUser2 = askUserRepository.Create(user2);
            context.SaveChanges();
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, Author = addedUser };
            var addedQuestion = questionRepository.Create(question);
            context.SaveChanges();
            var answer = new AnswerTO { Message = "En fait, c'est facile il faut toujorus faire des tests", Author = addedUser2, AssociatedQuestion = addedQuestion, };

            Assert.ThrowsException<ArgumentException>(() => answerRepository.Modify(answer));
        }

        [TestMethod]
        public void ModifyAnswer_ProvidingNull_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            Assert.ThrowsException<ArgumentNullException>(() => answerRepository.Modify(null));
        }

        [TestMethod]
        public void ModifyAnswer_ProvidingNonExistingId_ThrowException()
        {
            var options = new DbContextOptionsBuilder<AskContext>()
                    .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                    .Options;
            using var context = new AskContext(options);
            IAskUserRepository askUserRepository = new AskUserRepository(context);
            IQuestionRepository questionRepository = new QuestionRepository(context);
            IAnswerRepository answerRepository = new AnswerRepository(context);

            var user = new AskUserTO { FirstName = "Jean-Claude", LastName = "DuPet" };
            var user2 = new AskUserTO { FirstName = "Martine", LastName = "ALaPlage" };
            var addedUser = askUserRepository.Create(user);
            var addedUser2 = askUserRepository.Create(user2);
            context.SaveChanges();
            DateTime date = DateTime.Now;
            var question = new QuestionTO { IsArchived = false, Message = "Je n'arrive pas à faire un test", Title = "Problème avec Tests", Date = date, Author = addedUser };
            var addedQuestion = questionRepository.Create(question);
            context.SaveChanges();
            var answer = new AnswerTO {Id=16783, Message = "En fait, c'est facile il faut toujorus faire des tests", Author = addedUser2, AssociatedQuestion = addedQuestion, };

            Assert.ThrowsException<KeyNotFoundException>(() => answerRepository.Modify(answer));
        }
    }
}
