using System;

namespace WeedTunes.Dto.RequestDto.GenreDTO
{
    public class AddGenreToSongDTO
    {
        public Guid SongId { get; set; }
        public Guid GenreId { get; set; }
    }
}
