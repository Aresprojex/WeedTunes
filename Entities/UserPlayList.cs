using System;
using System.Collections.Generic;

namespace WeedTunes.Entities
{
    public class UserPlayList : BaseEntity
    {
        public string Name { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Guid ApplicationUserId { get; set; }
        public List<UserPlayListSong> UserPlayListSongs { get; set; } = new List<UserPlayListSong>(); 

    }
}
