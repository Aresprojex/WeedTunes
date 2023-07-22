using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeedTunes.Dto.RequestDto.SongDTOs
{
    public class CreateSongDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public DateTime ReleasedDate { get; set; }
    }

    public class AddStrainToSongDTO
    {
        [Required]
        public Guid SongId { get; set; }
        [Required]
        public Guid StrainId { get; set; }
    }

    public class AddSongsToPlayListDTO 
    {
        public Guid PlayListId { get; set; }
        public List<Guid> SongIds { get; set; } = new List<Guid>();

    }
}
