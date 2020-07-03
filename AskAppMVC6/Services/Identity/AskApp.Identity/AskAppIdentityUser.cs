using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Identity
{
    public class AskAppIdentityUser : IdentityUser<int>
    {
        public AskAppIdentityUser() : base()
        { }

        public AskAppIdentityUser(string userName) : base(userName) 
        { }
    }
}
