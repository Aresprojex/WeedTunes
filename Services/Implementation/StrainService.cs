using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Data;
using WeedTunes.Dto;
using WeedTunes.Entities;
using WeedTunes.Utilities;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Services
{
    public class StrainService : IStrainService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public StrainService(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        /// <summary>
        /// This method creates a new Strain in the database
        /// based on the information provided in the CreateStrianDto parameter.
        /// </summary>
        /// <param name="strianDto">The DTO that contains the information for
        /// the new Strain to be created.</param>
        /// <returns>A BaseResponse object that contains a StrainDto object in the
        /// Data property if the creation is successful, or an error message in the
        /// Errors property if the Strain name already exists.</returns>
        public async Task<BaseResponse<StrainDto>> CreateStrain(CreateStrianDto strianDto)
        {
            var response = new BaseResponse<StrainDto>();

            var strainNameExist = await _dbContext.Strains.FirstAsync(x => x.Name.ToLower() == strianDto.Name.ToLower());

            if(strainNameExist != null)
            {
                response.Errors.Add($"Strian with name {strianDto.Name} already exist");
                return response;
            }

            var strain = _mapper.Map<Strain>(strianDto);

            _dbContext.Strains.Add(strain);
            await _dbContext.SaveChangesAsync();

            var data = _mapper.Map<StrainDto>(strain);

            response.Data = data;
            return response;
        }

        public async Task<BaseResponse<StrainDto>> GetStrain(Guid id)
        {
            var response = new BaseResponse<StrainDto>();

            var strain = await _dbContext.Strains.FirstOrDefaultAsync(x => x.Id == id);

            if(strain == null)
            {
                response.Errors.Add("Strain not found");
                return response;
            }


            response.Data = _mapper.Map<StrainDto>(strain);
            return response;
        }

        public async Task<BaseResponse<PagedList<StrainDto>>> GetStrains(BaseSearchViewModel model)
        {
            var strains = _dbContext.Strains.AsQueryable();

            var pagedStrain = await EntityFilter(strains, model).ToPagedListAsync(model.PageIndex, model.PageSize);

            var cardsDto = _mapper.Map<IPagedList<Strain>, List<StrainDto>>(pagedStrain);

            var data = new PagedList<StrainDto>(cardsDto, model.PageIndex, model.PageSize, pagedStrain.TotalItemCount);

            return new BaseResponse<PagedList<StrainDto>> { Data = data, TotalCount = data.TotalItemCount, ResponseMessage = $"FOUND {cardsDto.Count} CARD(s)." };

        }

        private IQueryable<Strain> EntityFilter(IQueryable<Strain> query, BaseSearchViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Keyword) && !string.IsNullOrEmpty(model.Filter))
            {
                switch (model.Filter)
                {
                    case "name":
                        {
                            return query.Where(x => x.Name.ToLower().Contains(model.Keyword.ToLower()));
                        }

                    default:
                        break;
                }
            }

            return query;
        }
    }
}
