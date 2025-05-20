namespace Stargate.Application.Common.Mapping;

public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<Person, PersonDto>().ReverseMap();
    }
}