using System;
using WeedTunes.Entities;

namespace WeedTunes.Dto.ResponseDto.GenreDTO
{
    public class GenreDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        public static implicit operator GenreDTO(Genre model)
        {
            return model is null ? null
               : new GenreDTO
               {
                   Id = model.Id,
                   Name = model.Name,
                   DateCreated = model.CreatedOn
               };
        }
    }
}
