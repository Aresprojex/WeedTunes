using System;

namespace WeedTunes.Entities
{
    public class Song : BaseEntity
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public DateTime ReleasedDate { get; set; }
        public Genre Genre { get; set; }
        public Guid? GenreId { get; set; }
        public PlayList PlayList { get; set; }
        public Guid? PlayListId { get; set; }
        public Strain Strain { get; set; }
        public Guid? StrainId { get; set; }

    }
}
