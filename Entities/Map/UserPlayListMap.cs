using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WeedTunes.Models.Constant;

namespace WeedTunes.Entities.Map
{
    public class UserPlayListMap : IEntityTypeConfiguration<UserPlayList>
    {

        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void Configure(EntityTypeBuilder<UserPlayList> builder)
        {
            SetUpUserPlayList(builder);
        }

        private void SetUpUserPlayList(EntityTypeBuilder<UserPlayList> builder)
        {
            var playList = new UserPlayList
            {
                Id = Guid.Parse("12599d1b-8d12-49a6-a3ab-beeeaabb6661"),
                Name = "First Play List",
                ApplicationUserId = Defaults.SysUserId
            };

            builder.HasData(playList);
        }
    }
  
}
