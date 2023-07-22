using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeedTunes.Dto.RequestDto.UserPlayListDTO
{
    public class CreateUserPlayListDTO
    {
        [Required]
        public string Name { get; set; }
        public Guid UserId { get; set; }

    }

    public class AddSongToUserPlayListDTO 
    {
        public Guid UserId { get; set; }
        public Guid PlayListId { get; set; }   
        public Guid SongId { get; set; }
    }

    public class AddSongsToUserPlayListDTO
    {
        public Guid PlayListId { get; set; }
        public List<Guid> SongIds { get; set; } = new List<Guid>();

    }

}
