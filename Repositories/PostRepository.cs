using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repositories;

public class PostRepository : IPostRepository
{
    private readonly BlogDbContext _context;

    public PostRepository(BlogDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId)
    {
        return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<Post?> GetPostAsync(Guid id)
    {
        return await _context.Posts.FindAsync(id);
    }

    public async Task<Post> CreatePostAsync(CreatePostDto dto)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Content = dto.Content,
            UserId = dto.UserId,
            CreatedDate = DateTimeOffset.UtcNow
        };

        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> UpdatePostAsync(Guid id, UpdatePostDto dto)
    {
        var post = await GetPostAsync(id);
        if (post is null)
        {
            return post;
        }

        _context.Posts.Entry(post).CurrentValues.SetValues(dto);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> DeletePostAsync(Guid id)
    {
        var post = await GetPostAsync(id);
        if (post is null)
        {
            return post;
        }

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return post;
    }
}