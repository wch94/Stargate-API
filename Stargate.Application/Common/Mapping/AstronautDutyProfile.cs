namespace Stargate.Application.Common.Mapping;

public class AstronautDutyProfile : Profile
{
    public AstronautDutyProfile()
    {
        CreateMap<AstronautDuty, AstronautDutyDto>()
            .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.Person.Name));
    }
}
