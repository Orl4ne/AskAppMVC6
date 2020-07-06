using AskApp.Common.Interfaces;
using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.Ask.BLL
{
    public partial class AskUC :IAskUC
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

        public QuestionTO MarkMyQuestionAsResolved(int QuestionId)
        {
            var question = questionRepository.GetById(QuestionId);
            question.IsArchived = true;
            return questionRepository.Modify(question);
        }

        public List<QuestionTO> ShowAllQuestions()
        {
            return questionRepository.GetAll();
        }

        public List<QuestionTO> ShowMyQuestions(int UserId)
        {
            return questionRepository.GetAll().Where(x => x.Author.Id == UserId).ToList();
        }

        public QuestionTO ShowThisQuestion(int QuestionId)
        {
            return questionRepository.GetById(QuestionId);
        }
    }
}
