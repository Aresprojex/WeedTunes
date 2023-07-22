using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WeedTunes.Models.Constant;

namespace WeedTunes.Entities.Map
{
    public class UserFavouriteSongMap : IEntityTypeConfiguration<UserFavouriteSong>
    {

        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<UserFavouriteSong> builder)
        {
            SetUpUserFavouriteSong(builder);
        }

        private void SetUpUserFavouriteSong(EntityTypeBuilder<UserFavouriteSong> builder)
        {
            var userFavouriteSong = new UserFavouriteSong
            {
                Id = Guid.Parse("fba22b76-77be-4af0-ae54-2db7785d40ec"),
                ApplicationUserId = Defaults.SysUserId,
                SongId = Guid.Parse("edb4a662-b4af-4001-82a4-0405c44f8d98")
            };

            builder.HasData(userFavouriteSong);
        }
    }
}
