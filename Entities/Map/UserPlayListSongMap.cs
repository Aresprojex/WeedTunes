using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using WeedTunes.Models.Constant;

namespace WeedTunes.Entities.Map
{
    public class UserPlayListSongMap : IEntityTypeConfiguration<UserPlayListSong>
    {

        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<UserPlayListSong> builder)
        {
            SetUpUserPlayListSong(builder);
        }

        private void SetUpUserPlayListSong(EntityTypeBuilder<UserPlayListSong> builder)
        {
            var userPlayList = new UserPlayListSong
            {
                Id = Guid.Parse("5511aed4-bb5c-4643-880c-393ddd07a907"),
                UserPlayListId = Guid.Parse("12599d1b-8d12-49a6-a3ab-beeeaabb6661"),
                SongId = Guid.Parse("edb4a662-b4af-4001-82a4-0405c44f8d98")
            };

            builder.HasData(userPlayList);
        }
    }
}
