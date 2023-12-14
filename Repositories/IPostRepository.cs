using BlogApi.Models;

namespace BlogApi.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPostsAsync();

    Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId);

    Task<Post?> GetPostAsync(Guid id);

    Task<Post> CreatePostAsync(CreatePostDto dto);

    Task<Post?> UpdatePostAsync(Guid id, UpdatePostDto dto);

    Task<Post?> DeletePostAsync(Guid id);
}