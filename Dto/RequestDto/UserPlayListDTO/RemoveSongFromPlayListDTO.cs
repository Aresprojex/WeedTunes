using System;

namespace WeedTunes.Dto.RequestDto.UserPlayListDTO
{
    public class RemoveSongFromPlayListDTO
    {
        public Guid UserPlayListId { get; set; }
        public Guid SongId { get; set; }
    }
}
