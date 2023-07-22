using System;
using System.Collections.Generic;

namespace WeedTunes.Entities
{
    public class PlayList : BaseEntity
    {
        public string Name { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>(); 
    }
}
