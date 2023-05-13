using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeedTunes.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
            Id = Guid.NewGuid();
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }

    }
}
