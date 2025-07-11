namespace CoreFlow.Application.Services;

public class UserService(IUserRepository userRepository, ISystemRepository systemRepository, IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ISystemRepository _systemRepository = systemRepository;
    private readonly IMapper _mapper = mapper;

    private async Task<UserDto> ToDtoAsync(Guid id)
    {
        User user = await _userRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        List<Guid> systemIds = [.. user.UserSystems.Select(us => us.SystemId)];

        List<SystemDto> systemDtos = new(systemIds.Count);
        foreach (Guid sid in systemIds)
        {
            Domain.Entities.System? system = await _systemRepository.GetByIdAsync(sid);
            if (system == null)
            {
                continue;
            }
            systemDtos.Add(_mapper.Map<SystemDto>(system));
        }
        return new UserDto(user.Id, user.LastName, user.FirstName, user.MiddleName, user.UserName, user.IsAdmin, systemDtos);
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        return await ToDtoAsync(id);
    }

    public async Task<UserDto> GetByUserNamePasswordAsync(string userName, string password)
    {
        User user = await _userRepository.GetByUserNamePasswordAsync(userName, password) ?? throw new KeyNotFoundException();
        return await ToDtoAsync(user.Id);
    }

    public async Task<IReadOnlyCollection<UserDto>> GetAllAsync()
    {
        List<User> users = await _userRepository.GetAllAsync();
        List<UserDto> userDtos = new(users.Count);

        foreach (User user in users)
        {
            userDtos.Add(await ToDtoAsync(user.Id));
        }

        return userDtos;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        if (await _userRepository.ExistsByUserNameAsync(dto.UserName))
        {
            throw new InvalidOperationException("User name taken.");
        }

        User user = new(dto.LastName, dto.FirstName, dto.MiddleName, dto.UserName, dto.Password, dto.IsAdmin);

        foreach (Guid systemId in dto.SystemIds)
        {
            if (await _systemRepository.ExistsAsync(systemId))
            {
                throw new KeyNotFoundException($"System {systemId} not found.");
            }
            user.AddSystem(systemId);
        }

        await _userRepository.CreateAsync(user);

        return await ToDtoAsync(user.Id);
    }

    public async Task<UserDto> UpdateAsync(UpdateUserDto dto)
    {
        User user = await _userRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException("User not found.");

        if (dto.LastName != null)
        {
            user.LastName = dto.LastName;
        }

        if (dto.FirstName != null)
        {
            user.FirstName = dto.FirstName;
        }

        if (dto.MiddleName != null)
        {
            user.MiddleName = dto.MiddleName;
        }

        if (dto.IsAdmin != null)
        {
            user.SetAdmin(dto.IsAdmin.Value);
        }

        if (dto.UserName != null)
        {
            if (await _userRepository.ExistsByUserNameAsync(dto.UserName) && dto.UserName != user.UserName)
            {
                throw new InvalidOperationException("Username already taken.");
            }

            user.ChangeUserName(dto.UserName);
        }

        if (dto.Password != null)
        {
            user.ChangePassword(dto.Password);
        }

        if (dto.SystemIds != null)
        {
            foreach (UserSystem userSystem in user.UserSystems.ToList())
            {
                user.RemoveSystem(userSystem.SystemId);
            }

            foreach (Guid systemId in dto.SystemIds)
            {
                if (await _systemRepository.ExistsAsync(systemId))
                {
                    throw new KeyNotFoundException($"System {systemId} not found.");
                }

                user.AddSystem(systemId);
            }
        }

        await _userRepository.UpdateAsync(user);
        return await ToDtoAsync(user.Id);
    }

    public Task DeleteAsync(Guid id)
    {
        return _userRepository.DeleteAsync(id);
    }
}