﻿using AskApp.Ask.DAL.Extensions;
using AskApp.Common.Interfaces.IRepositories;
using AskApp.Common.TOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AskApp.Ask.DAL.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private AskContext askContext;
        public AnswerRepository(AskContext askContext)
        {
            this.askContext = askContext;
        }
        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            askContext.Dispose();
        }

        public AnswerTO Create(AnswerTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            if (entity.Id != 0)
            {
                return entity;
            }
            var answerEF = entity.ToEF();
            answerEF.AssociatedQuestion = askContext.Questions.First(x => x.Id == entity.AssociatedQuestion.Id && entity.AssociatedQuestion.IsResolved != true);

            var result = askContext.Answers.Add(answerEF);
            askContext.SaveChanges();

            return result.Entity.ToTransferObject();
        }

        public bool Delete(AnswerTO entity)
        {
            if (entity is null)
            {
                throw new KeyNotFoundException();
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Answer To Delete Invalid Id");
            }

            var answer = askContext.Answers.FirstOrDefault(x => x.Id == entity.Id);
            askContext.Answers.Remove(answer);
            askContext.SaveChanges();
            return true;
        }

        public List<AnswerTO> GetAll()
        {
            var list = askContext.Answers
                .Include(x=>x.AssociatedQuestion)
                ?.Select(x => x.ToTransferObject())
                .ToList();
            if (!list.Any())
            {
                throw new ArgumentNullException("There is no Answer in DB");
            }
            return list;
        }

        public AnswerTO GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Answer not found, invalid Id");
            }
            return askContext.Answers.FirstOrDefault(x => x.Id == id && x.AssociatedQuestion.IsResolved != true).ToTransferObject();
        }

        public AnswerTO Modify(AnswerTO entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (entity.Id <= 0)
            {
                throw new ArgumentException("Answer To Update Invalid Id");
            }
            if (!askContext.Answers.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Update(AnswerTO) Can't find Answer to update.");
            }

            var editedEntity = askContext.Answers.FirstOrDefault(e => e.Id == entity.Id && e.AssociatedQuestion.IsResolved != true);
            if (editedEntity != default)
            {
                entity.ToTrackedEF(editedEntity);
            }
            askContext.SaveChanges();

            return editedEntity.ToTransferObject();
        }
    }
}
