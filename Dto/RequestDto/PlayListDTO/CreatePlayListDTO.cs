using System.ComponentModel.DataAnnotations;

namespace WeedTunes.Dto.RequestDto.PlayListDTO
{
    public class CreatePlayListDTO
    {
        [Required]
        public string Name { get; set; } 
    }
}
