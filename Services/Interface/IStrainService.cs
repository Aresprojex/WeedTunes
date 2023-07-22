using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Dto;
using WeedTunes.Utilities;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Services
{
    public interface IStrainService
    {
        // Creates a strain and it's feeling and negative aspects
        Task<BaseResponse<StrainDto>> CreateStrain(CreateStrianDto strianDto);
        Task<BaseResponse<StrainDto>> GetStrain(Guid id);
        Task<BaseResponse<PagedList<StrainDto>>> GetStrains(BaseSearchViewModel search);
    }
}
