using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Common.Interfaces
{
    public interface IAskUC
    {
        List<QuestionTO> ShowAllQuestions();
        List<QuestionTO> ShowMyQuestions();
        QuestionTO AskAQuestion(QuestionTO Question);
        QuestionTO ShowThisQuestion(QuestionTO Question);
        AnswerTO AnsweringQuestion(QuestionTO Question, AnswerTO Answer);
        QuestionTO MarkMyQuestionAsResolved(QuestionTO Question);
    }
}
