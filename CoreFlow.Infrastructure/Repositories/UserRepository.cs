namespace CoreFlow.Infrastructure.Repositories;

public class UserRepository(CoreFlowContext coreFlowContext) : IUserRepository
{
    private readonly CoreFlowContext _coreFlowContext = coreFlowContext;

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _coreFlowContext
            .Users
            .AsNoTracking()
            .AnyAsync(predicate => predicate.Id == id);
    }

    public async Task<bool> ExistsByUserNameAsync(string userName)
    {
        return await _coreFlowContext
            .Users
            .AsNoTracking()
            .AnyAsync(predicate => predicate.UserName == userName);
    }

    public async Task<bool> ExistsByUserNameNotIdAsync(string userName, Guid id)
    {
        return await _coreFlowContext
            .Users
            .AsNoTracking()
            .AnyAsync(predicate => predicate.UserName == userName && predicate.Id != id);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _coreFlowContext
            .Users
            .Include(navigationPropertyPath => navigationPropertyPath.UserAppSystems)
            .ThenInclude(navigationPropertyPath => navigationPropertyPath.AppSystem)
            .FirstOrDefaultAsync(predicate => predicate.Id == id);
    }

    public async Task<User?> GetByUserNamePasswordAsync(string userName, string password)
    {
        return await _coreFlowContext
            .Users
            .Include(navigationPropertyPath => navigationPropertyPath.UserAppSystems)
            .ThenInclude(navigationPropertyPath => navigationPropertyPath.AppSystem)
            .FirstOrDefaultAsync(predicate => predicate.UserName == userName && predicate.Password == password);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _coreFlowContext
            .Users
            .Include(navigationPropertyPath => navigationPropertyPath.UserAppSystems)
            .ThenInclude(navigationPropertyPath => navigationPropertyPath.AppSystem)
            .ToListAsync();
    }

    public async Task<List<User>> SearchAsync()
    {
        return await _coreFlowContext
            .Users
            .ToListAsync();
    }

    public async Task CreateAsync(User user)
    {
        await _coreFlowContext.Users.AddAsync(user);
        await _coreFlowContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _coreFlowContext.Users.Update(user);
        await _coreFlowContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        User? user = await GetByIdAsync(id);
        if (user != null)
        {
            _coreFlowContext.Users.Remove(user);
            await _coreFlowContext.SaveChangesAsync();
        }
    }
}