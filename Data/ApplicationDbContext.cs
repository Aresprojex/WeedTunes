using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using WeedTunes.Entities;

namespace WeedTunes.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Strain> Strains { get; set; }
        public DbSet<StrainFeeling> StrainFeelings { get; set; }
        public DbSet<StrainFlavour> StrainFlavours { get; set; }
        public DbSet<StrainHelpsWith> StrainHelpsWith { get; set; }
        public DbSet<StrainNegativeAspect> StrainNegativeAspects { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<PlayList> PlayLists { get; set; }
        public DbSet<UserPlayList> UserPlayLists { get; set; } 
        public DbSet<UserPlayListSong> UserPlayListSongs { get; set; }
        public DbSet<UserFavouriteSong> UserFavouriteSongs { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
