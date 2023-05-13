using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Dto;

namespace WeedTunes.Services
{
    public interface IStrainService
    {
        // Creates a strain and it's feeling and negative aspects
        Task CreateStrain(CreateStrianDto strianDto);
    }
}
