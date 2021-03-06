﻿using AskApp.Common.TOs;
using AskApp.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskApp.Web.Models
{
    public class AnswerQuestionVM
    {
        public QuestionTO Question { get; set; }
        public AnswerTO Answer { get; set; }
        public AskAppIdentityUser AnswerAuthor { get; set; }
    }
}
