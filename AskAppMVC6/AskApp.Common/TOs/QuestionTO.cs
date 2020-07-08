using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Common.TOs
{
    public class QuestionTO
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsResolved { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
    }
}
