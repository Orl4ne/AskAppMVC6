using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Identity
{
    public class AskAppUserRole : IdentityRole<int>
    {
        public AskAppUserRole() : base()
        { }
        public AskAppUserRole(string userName) : base(userName)
        { }
    }
}
