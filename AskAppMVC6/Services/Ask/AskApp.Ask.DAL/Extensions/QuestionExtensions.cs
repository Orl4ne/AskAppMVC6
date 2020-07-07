using AskApp.Ask.DAL.Entities;
using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.DAL.Extensions
{
    public static class QuestionExtensions
    {
        public static QuestionTO ToTransferObject(this QuestionEF question)
        {
            if (question is null)
                throw new ArgumentNullException(nameof(question));

            return new QuestionTO
            {
                Id = question.Id,
                AuthorId = question.AuthorId,
                Date = question.Date,
                IsArchived = question.IsArchived,
                Message = question.Message,
                Title = question.Title,
            };
        }

        public static QuestionEF ToEF(this QuestionTO question)
        {
            if (question is null)
                throw new ArgumentNullException(nameof(question));

            return new QuestionEF
            {
                Id = question.Id,
                AuthorId = question.AuthorId,
                Date = question.Date,
                IsArchived = question.IsArchived,
                Message = question.Message,
                Title = question.Title,
            };
        }

        public static QuestionEF ToTrackedEF(this QuestionTO question, QuestionEF questionToModify)
        {
            if (questionToModify is null)
                throw new ArgumentNullException(nameof(questionToModify));
            if (question is null)
                throw new ArgumentNullException(nameof(question));

            questionToModify.Id = question.Id;
            questionToModify.AuthorId = question.AuthorId;
            questionToModify.Date = question.Date;
            questionToModify.IsArchived = question.IsArchived;
            questionToModify.Message = question.Message;
            questionToModify.Title = question.Title;

            return questionToModify;
        }
    }
}
