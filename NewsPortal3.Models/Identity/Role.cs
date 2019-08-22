using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal3.Models.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {

        }
        public Role(string name)
        {
            this.Name = name;
        }
    }
}
