﻿using AskApp.Common.TOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Common.Interfaces.IRepositories
{
    public interface IAnswerRepository : IRepository<AnswerTO, int>
    {
    }
}
