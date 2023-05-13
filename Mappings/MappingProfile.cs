using AutoMapper;
using WeedTunes.Dto;
using WeedTunes.Entities;

namespace WeedTunes.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // maps request dto to entities
            CreateMap<CreateStrianDto, Strain>();
            CreateMap<StrainFeelingDto, StrainFeeling>();
            CreateMap<StrainNegativeAspectDto, StrainNegativeAspect>();
            CreateMap<StrainHelpsWithDto, StrainHelpsWith>();
            CreateMap<StrainFlavourDto, StrainFlavour>();

            // maps entities to response dto
            CreateMap<Strain, StrainDto>();

        }
    }
}
