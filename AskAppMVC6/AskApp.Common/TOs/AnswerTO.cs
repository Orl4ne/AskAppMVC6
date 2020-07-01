using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Common.TOs
{
    public class AnswerTO
    {
        public int Id { get; set; }
        public UserTO Author { get; set; }
        public string Message { get; set; }
        public QuestionTO AssociatedQuestion { get; set; }
    }
}
