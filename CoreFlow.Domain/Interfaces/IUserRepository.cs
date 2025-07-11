namespace CoreFlow.Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByUserNameAsync(string userName);

    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUserNamePasswordAsync(string userName, string password);
    Task<List<User>> GetAllAsync();

    Task CreateAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(Guid id);
}