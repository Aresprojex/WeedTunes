using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Data;
using WeedTunes.Dto;
using WeedTunes.Entities;
using WeedTunes.Utilities.Helpers;

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
    }
}
