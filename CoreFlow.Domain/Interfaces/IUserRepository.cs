namespace CoreFlow.Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByUserNameAsync(string userName);
    Task<bool> ExistsByUserNameNotIdAsync(string userName, Guid ids);

    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUserNamePasswordAsync(string userName, string password);
    Task<List<User>> GetAllAsync();

    Task<List<User>> SearchAsync(string searchString);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(Guid id);
}