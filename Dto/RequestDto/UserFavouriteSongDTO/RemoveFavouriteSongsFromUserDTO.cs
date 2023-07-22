using System.Collections.Generic;
using System;

namespace WeedTunes.Dto.RequestDto.UserFavouriteSongDTO
{
    public class RemoveFavouriteSongsFromUserDTO
    {
        public List<Guid> SongIds { get; set; } = new List<Guid>();
    }
}
