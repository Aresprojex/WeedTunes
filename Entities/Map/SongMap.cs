using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace WeedTunes.Entities.Map
{
    public class SongMap : IEntityTypeConfiguration<Song>
    {
        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<Song> builder)
        {      
            SetUpSong(builder);
        }

        public void SetUpSong(EntityTypeBuilder<Song> builder)
        {
            var song1 = new Song
            {
                Id = Guid.Parse("edb4a662-b4af-4001-82a4-0405c44f8d98"),
                Name = "Smokestack Lightnin",
                Artist = "Howlin Wolf"

            };

            var song2 = new Song
            {
                Id = Guid.Parse("64a18e8b-ec2b-4631-8f21-49a0dbd1451f"),
                Name = "Hotel California",
                Artist = "Eagles"
            };

            var song3 = new Song
            {
                Id = Guid.Parse("973AF7A9-7F18-4E8B-ACD3-BC906580561A"),
                Name = "I Will Always Love You",
                Artist = "Whitney Houston"
            };

            var song4 = new Song
            {
                Id = Guid.Parse("2808568f-f79f-4b6e-8a75-7ee2f0700636"),
                Name = "Spoonful",
                Artist = "Howlin Wolf"
            };

            var song5 = new Song
            {
                Id = Guid.Parse("0104cf12-fff0-472f-930f-ee6fe2f1dc8f"),
                Name = "Killing Floor",
                Artist = "Howlin Wolf"
            };

            var songs = new List<Song> { song1, song2, song3, song4, song5 };
            builder.HasData(songs);

        }


    }
}
