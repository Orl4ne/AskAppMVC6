using AskApp.Common.Interfaces;
using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.Ask.BLL
{
    public partial class AskUC : IAskUC
    {
        public AnswerTO AnsweringQuestion(int QuestionId, AnswerTO Answer)
        {
            var question = questionRepository.GetById(QuestionId);
            Answer.AssociatedQuestion = question;
            return answerRepository.Create(Answer);
        }

        public QuestionTO AskAQuestion(QuestionTO Question)
        {
            var date = DateTime.Now;
            Question.Date = date;
            return questionRepository.Create(Question);
        }

        public QuestionTO DeletingQuestion(int UserId, QuestionTO Question)
        {
            if (UserId == Question.AuthorId)
            {
                Question.IsDeleted = true;
                var modifiedEntity = questionRepository.Modify(Question);
                return modifiedEntity;
            }
            else

                throw new Exception("You can't delete this question because your are not the author of this question");
        }

        public List<AnswerTO> GetAnswersByQuestion(int QuestionId)
        {
            try
            {
                return answerRepository.GetAll().Where(a => a.AssociatedQuestion.Id == QuestionId).ToList();
            }
            catch (Exception)
            {

                return new List<AnswerTO>();
            }
        }

        public QuestionTO MarkMyQuestionAsResolved(int QuestionId)
        {
            var question = questionRepository.GetById(QuestionId);
            question.IsResolved = true;
            return questionRepository.Modify(question);
        }

        public List<QuestionTO> ShowAllQuestions()
        {
            return questionRepository.GetAll();
        }

        public List<QuestionTO> ShowMyQuestions(int UserId)
        {
            return questionRepository.GetAll().Where(x => x.AuthorId == UserId).ToList();
        }

        public QuestionTO ShowThisQuestion(int QuestionId)
        {
            return questionRepository.GetById(QuestionId);
        }
    }
}
