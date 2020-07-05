using AskApp.Common.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.BLL
{
    public partial class AskUC
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IAskUserRepository askUserRepository;

        public AskUC(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository ?? throw new System.ArgumentNullException(nameof(questionRepository));
        }

        public AskUC(IAnswerRepository answerRepository)
        {
            this.answerRepository = answerRepository ?? throw new System.ArgumentNullException(nameof(answerRepository));
        }

        public AskUC(IAskUserRepository askUserRepository)
        {
            this.askUserRepository = askUserRepository ?? throw new System.ArgumentNullException(nameof(askUserRepository));
        }
    }
}
