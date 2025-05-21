namespace Stargate.Application.Common.Mapping;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonAstronautDto>()
            .ForMember(dest => dest.CurrentRank,
                opt => opt.MapFrom(src => src.AstronautDetail != null ? src.AstronautDetail.CurrentRank : string.Empty))
            .ForMember(dest => dest.CurrentDutyTitle,
                opt => opt.MapFrom(src => src.AstronautDetail != null ? src.AstronautDetail.CurrentDutyTitle : string.Empty))
            .ForMember(dest => dest.CareerStartDate,
                opt => opt.MapFrom(src => src.AstronautDetail != null ? src.AstronautDetail.CareerStartDate : (DateTime?)null))
            .ForMember(dest => dest.CareerEndDate,
                opt => opt.MapFrom(src => src.AstronautDetail != null ? src.AstronautDetail.CareerEndDate : null));

        CreateMap<AstronautDuty, AstronautDutyDto>()
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.Name));
    }
}