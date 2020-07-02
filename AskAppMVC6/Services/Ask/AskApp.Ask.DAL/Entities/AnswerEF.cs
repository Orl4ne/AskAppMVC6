using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.DAL.Entities
{
    public class AnswerEF
    {
        public int Id { get; set; }
        public AskUserEF Author { get; set; }
        public string Message { get; set; }
        public QuestionEF AssociatedQuestion { get; set; }
    }
}

