using System;

namespace WeedTunes.Entities
{
    public class UserFavouriteSong : BaseEntity
    {
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; } 
    }
}
