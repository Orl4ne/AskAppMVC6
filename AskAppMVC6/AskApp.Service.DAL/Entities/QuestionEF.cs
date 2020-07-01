using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Ask.DAL.Entities
{
    public class QuestionEF
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsArchived { get; set; }
        public DateTime Date { get; set; }
    }
}
