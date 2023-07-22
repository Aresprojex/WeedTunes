using System;

namespace WeedTunes.Entities
{
    public class UserPlayListSong : BaseEntity
    {
        public UserPlayList UserPlayList { get; set; }
        public Guid UserPlayListId { get; set; }
        public Song Song { get; set; }
        public Guid? SongId { get; set; }  
    }
}
