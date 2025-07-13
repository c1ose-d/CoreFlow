using CoreFlow.Application.DTOs.AppSystem;

namespace CoreFlow.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        _ = CreateMap<User, UserDto>();
        _ = CreateMap<CreateUserDto, User>();
        _ = CreateMap<UpdateUserDto, User>();

        _ = CreateMap<Domain.Entities.AppSystem, AppSystemDto>();
        _ = CreateMap<CreateAppSystemDto, Domain.Entities.AppSystem>();
        _ = CreateMap<UpdateAppSystemDto, Domain.Entities.AppSystem>();
    }
}