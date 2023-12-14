using BlogApi.Models;

namespace BlogApi.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();

    Task<User?> GetUserAsync(Guid id);

    Task<User> CreateUserAsync(CreateUserDto dto);

    Task<User?> UpdateUserAsync(Guid id, UpdateUserDto dto);

    Task<User?> DeleteUserAsync(Guid id);
}