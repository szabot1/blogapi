using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BlogDbContext _context;

    public UserRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users
        .Include(u => u.Posts)
        .ToListAsync();
    }

    public async Task<User?> GetUserAsync(Guid id)
    {
        return await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateUserAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            Password = dto.Password,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(Guid id, UpdateUserDto dto)
    {
        var user = await GetUserAsync(id);
        if (user is null)
        {
            return user;
        }

        _context.Users.Entry(user).CurrentValues.SetValues(dto);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteUserAsync(Guid id)
    {
        var user = await GetUserAsync(id);
        if (user is null)
        {
            return user;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
}