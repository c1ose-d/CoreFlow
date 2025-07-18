namespace CoreFlow.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        _ = CreateMap<User, UserDto>();
        _ = CreateMap<CreateUserDto, User>();
        _ = CreateMap<UpdateUserDto, User>();

        _ = CreateMap<AppSystem, AppSystemDto>();
        _ = CreateMap<CreateAppSystemDto, AppSystem>();
        _ = CreateMap<UpdateAppSystemDto, AppSystem>();

        _ = CreateMap<ServerBlock, ServerBlockDto>()
            .ForMember(destinationMember => destinationMember.Servers, memberOptions => memberOptions.MapFrom(mapExpression => mapExpression.Servers))
            .MaxDepth(2)
            .PreserveReferences();
        _ = CreateMap<CreateServerBlockDto, ServerBlock>();
        _ = CreateMap<UpdateServerBlockDto, ServerBlock>();

        _ = CreateMap<Server, ServerDto>()
            .ConstructUsing((src, ctx) =>
            {
                if (!ctx.TryGetItems(out Dictionary<string, object>? items)
                || !items.TryGetValue("_encryptionService", out object? svcObj))
                {
                    throw new InvalidOperationException("_encryptionService не передан");
                }
                IEncryptionService enc = (IEncryptionService)svcObj;
                string plain = enc.Decrypt(src.Password);
                ServerBlock block = src.ServerBlock;
                ServerBlockDto blockDto = new(block.Id, block.Name, block.AppSystemId, []);

                return new ServerDto(src.Id, src.IpAddress, src.HostName, src.UserName, plain, blockDto);
            })
            .MaxDepth(2)
            .PreserveReferences();
        _ = CreateMap<UpdateServerDto, Server>();
        _ = CreateMap<CreateServerDto, Server>();
    }
}