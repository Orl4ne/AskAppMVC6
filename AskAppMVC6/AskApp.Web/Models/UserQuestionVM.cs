using AskApp.Common.TOs;
using AskApp.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskApp.Web.Models
{
    public class UserQuestionVM
    {
        public QuestionTO Question { get; set; }
        public AskAppIdentityUser User { get; set; }
        public List<AnswerTO> Answers { get; set; }
    }
}
