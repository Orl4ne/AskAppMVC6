using AskApp.Ask.DAL.Entities;
using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.DAL.Extensions
{
    public static class AnswerExtensions
    {
        public static AnswerTO ToTransferObject(this AnswerEF answer)
        {
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));

            return new AnswerTO
            {
                Id = answer.Id,
                AuthorId = answer.AuthorId,
                Message = answer.Message,
                AssociatedQuestion = answer.AssociatedQuestion.ToTransferObject(),
            };
        }

        public static AnswerEF ToEF(this AnswerTO answer)
        {
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));

            return new AnswerEF
            {
                Id = answer.Id,
                AuthorId = answer.AuthorId,
                Message = answer.Message,
                AssociatedQuestion = answer.AssociatedQuestion.ToEF(),
            };
        }

        public static AnswerEF ToTrackedEF(this AnswerTO answer, AnswerEF answerToModify)
        {
            if (answerToModify is null)
                throw new ArgumentNullException(nameof(answerToModify));
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));

            answerToModify.Id = answer.Id;
            answerToModify.AuthorId = answer.AuthorId;
            answerToModify.Message = answer.Message;
            answerToModify.AssociatedQuestion = answer.AssociatedQuestion.ToTrackedEF(answerToModify.AssociatedQuestion);

            return answerToModify;
        }
    }
}
