using AskApp.Common.Interfaces;
using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.BLL
{
    public partial class AskUC : IAskUC
    {
        public AnswerTO AnsweringQuestion(QuestionTO Question, AnswerTO Answer)
        {
            throw new NotImplementedException();
        }

        public QuestionTO AskAQuestion(QuestionTO Question)
        {
            throw new NotImplementedException();
        }

        public QuestionTO MarkMyQuestionAsResolved(QuestionTO Question)
        {
            throw new NotImplementedException();
        }

        public List<QuestionTO> ShowAllQuestions()
        {
            throw new NotImplementedException();
        }

        public List<QuestionTO> ShowMyQuestions()
        {
            throw new NotImplementedException();
        }

        public QuestionTO ShowThisQuestion(QuestionTO Question)
        {
            throw new NotImplementedException();
        }
    }
}
