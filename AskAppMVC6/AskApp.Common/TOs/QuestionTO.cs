using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Common.TOs
{
    public class QuestionTO
    {
        public int Id { get; set; }
        public AskUserTO Author { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsArchived { get; set; }
        public DateTime Date { get; set; }
    }
}
