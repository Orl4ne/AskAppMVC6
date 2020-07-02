using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Identity
{
    public class User : IdentityUser<int>
    {
        public int Id { get; set; }
    }
}
