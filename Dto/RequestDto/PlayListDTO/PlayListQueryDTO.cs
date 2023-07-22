using System;
using System.ComponentModel.DataAnnotations;
using WeedTunes.Utilities;

namespace WeedTunes.Dto.RequestDto.PlayListDTO
{
    public class PlayListQueryDTO : BaseSearchViewModel
    {
        [Required]
        public Guid PlayListId { get; set; }  
    }
}
