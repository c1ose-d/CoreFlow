namespace CoreFlow.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        _ = CreateMap<User, UserDto>();
        _ = CreateMap<CreateUserDto, User>();
        _ = CreateMap<UpdateUserDto, User>();

        _ = CreateMap<Domain.Entities.System, SystemDto>();
        _ = CreateMap<CreateSystemDto, Domain.Entities.System>();
        _ = CreateMap<UpdateSystemDto, Domain.Entities.System>();
    }
}