using AskApp.Ask.DAL.Extensions;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace AskApp.Ask.DAL.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private AskContext askContext;
        public QuestionRepository(AskContext askContext)
        {
            this.askContext = askContext;
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            askContext.Dispose();
        }

        public QuestionTO Create(QuestionTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            if (entity.Id != 0)
            {
                return entity;
            }
            var questionEF = entity.ToEF();

            var result = askContext.Questions.Add(questionEF);
            askContext.SaveChanges();

            return result.Entity.ToTransferObject();
        }

        public bool Delete(QuestionTO entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Question To Delete Invalid Id");
            }

            var question = askContext.Questions.FirstOrDefault(x => x.Id == entity.Id);
            askContext.Questions.Remove(question);
            askContext.SaveChanges();
            return true;
        }

        public List<QuestionTO> GetAll()
        {
            var list = askContext.Questions.AsEnumerable()
                .Where(r=>r.IsArchived!= true)
                ?.Select(x => x.ToTransferObject())
                .ToList();
            if (!list.Any())
            {
                throw new ArgumentNullException("There is no Question in DB");
            }
            return list;
        }

        public QuestionTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Question not found, invalid Id");
            }
            return askContext.Questions.FirstOrDefault(x => x.Id == id).ToTransferObject();
        }

        public QuestionTO Modify(QuestionTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Question To Update Invalid Id");
            }
            if (!askContext.Questions.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Update(QuestionTO) Can't find Question to update.");
            }

            var editedEntity = askContext.Questions.FirstOrDefault(e => e.Id == entity.Id);
            if (editedEntity != default)
            {
                entity.ToTrackedEF(editedEntity);
            }
            askContext.SaveChanges();

            return editedEntity.ToTransferObject();
        }
    }
}
