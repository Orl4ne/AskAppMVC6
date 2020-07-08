using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Common.Interfaces
{
    public interface IAskUC
    {
        List<QuestionTO> ShowAllQuestions();
        List<QuestionTO> ShowMyQuestions(int UserId);
        QuestionTO AskAQuestion(QuestionTO Question);
        QuestionTO ShowThisQuestion(int QuestionId);
        AnswerTO AnsweringQuestion(int QuestionId, AnswerTO Answer);
        QuestionTO MarkMyQuestionAsResolved(int QuestionId);
        List<AnswerTO> GetAnswersByQuestion(int QuestionId);
        QuestionTO DeletingQuestion(int UserId, QuestionTO Question);

    }
}
