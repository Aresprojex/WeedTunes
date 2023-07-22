using System;
using System.Collections.Generic;

namespace WeedTunes.Dto.RequestDto.UserFavouriteSongDTO
{
    public class AddFavouriteSongsToUserDTO
    {
        public Guid UserId { get; set; }
        public List<Guid> SongIds { get; set; } = new List<Guid>(); 
    }
}
