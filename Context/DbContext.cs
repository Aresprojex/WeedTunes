using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WeedTunes.Entities;

namespace WeedTunes.Context
{
    public class DbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {

        }

        public DbSet<Strain> Strains { get; set; }

    }
}
