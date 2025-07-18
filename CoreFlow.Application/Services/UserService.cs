namespace CoreFlow.Application.Services;

public class UserService(IUserRepository userRepository, IAppSystemRepository systemRepository, IMapper mapper) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IAppSystemRepository _systemRepository = systemRepository;
    private readonly IMapper _mapper = mapper;

    private async Task<UserDto> ToDtoAsync(Guid id)
    {
        User user = await _userRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        List<Guid> systemIds = [.. user.UserAppSystems.Select(us => us.AppSystemId)];

        List<AppSystemDto> systemDtos = new(systemIds.Count);
        foreach (Guid sid in systemIds)
        {
            AppSystem? appSystem = await _systemRepository.GetByIdAsync(sid);
            if (appSystem == null)
            {
                continue;
            }
            systemDtos.Add(_mapper.Map<AppSystemDto>(appSystem));
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

    public async Task<IReadOnlyCollection<UserDto>> SearchAsync(string searchString)
    {
        List<User> users = await _userRepository.SearchAsync();
        List<UserDto> userDtos = new(users.Count);

        foreach (User user in users)
        {
            userDtos.Add(await ToDtoAsync(user.Id));
        }

        return [.. userDtos.Where(predicate => predicate.UserName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase) || predicate.FullName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))];
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        if (await _userRepository.ExistsByUserNameAsync(dto.UserName))
        {
            throw new InvalidOperationException("User name taken.");
        }

        User user = new(dto.LastName, dto.FirstName, dto.MiddleName, dto.UserName, dto.Password, dto.IsAdmin);

        foreach (Guid appSystemId in dto.AppSystemIds)
        {
            AppSystem appSystem = await _systemRepository.GetByIdAsync(appSystemId) ?? throw new KeyNotFoundException($"System {appSystemId} not found.");
            user.AddAppSystem(appSystem);
        }

        await _userRepository.CreateAsync(user);

        return await ToDtoAsync(user.Id);
    }

    public async Task<UserDto> UpdateAsync(UpdateUserDto dto)
    {
        User user = await _userRepository.GetByIdAsync(dto.Id) ?? throw new KeyNotFoundException("User not found.");

        bool isChangeUserName = false;
        if (dto.UserName != null)
        {
            isChangeUserName = !await _userRepository.ExistsByUserNameNotIdAsync(dto.UserName, user.Id)
                ? true
                : throw new InvalidOperationException("User name already taken.");
        }

        user.Update(dto.LastName, dto.FirstName, dto.MiddleName, dto.UserName != null && isChangeUserName ? dto.UserName : null);

        if (dto.IsAdmin != null)
        {
            user.SetAdmin(dto.IsAdmin.Value);
        }

        if (dto.Password != null)
        {
            user.ChangePassword(dto.Password);
        }

        if (dto.AppSystemIds != null)
        {
            foreach (UserAppSystem userSystem in user.UserAppSystems.ToList())
            {
                user.RemoveAppSystem(userSystem.AppSystemId);
            }

            foreach (Guid systemId in dto.AppSystemIds)
            {
                AppSystem appSystem = await _systemRepository.GetByIdAsync(systemId) ?? throw new KeyNotFoundException($"System {systemId} not found.");
                user.AddAppSystem(appSystem);
            }
        }

        await _userRepository.UpdateAsync(user);
        return await ToDtoAsync(user.Id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _userRepository.DeleteAsync(id);
    }
}