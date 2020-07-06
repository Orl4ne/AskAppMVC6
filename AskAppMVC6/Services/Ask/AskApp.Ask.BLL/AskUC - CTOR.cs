using AskApp.Common.Interfaces;
using AskApp.Common.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.BLL
{
    public partial class AskUC : IAskUC
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
        public AskUC(IAnswerRepository answerRepository, IQuestionRepository questionRepository )
        {
            this.questionRepository = questionRepository ?? throw new System.ArgumentNullException(nameof(questionRepository));
            this.answerRepository = answerRepository ?? throw new System.ArgumentNullException(nameof(answerRepository));
            
        }

    }
}
