using System.ComponentModel.DataAnnotations;

namespace WeedTunes.Dto.RequestDto.GenreDTO
{
    public class CreateGenreDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
