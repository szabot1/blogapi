using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models;

public record Post
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    public string? Title { get; init; }

    [Required]
    public string? Content { get; init; }

    public Guid? UserId { get; init; }

    public User? User { get; init; }

    public DateTimeOffset CreatedDate { get; init; }
}

public record PostDto
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
    public User? User { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}

public record CreatePostDto
{
    [Required]
    public string? Title { get; init; }

    [Required]
    public string? Content { get; init; }

    [Required]
    public Guid? UserId { get; init; }
}

public record UpdatePostDto
{
    [Required]
    public string? Title { get; init; }

    [Required]
    public string? Content { get; init; }

    [Required]
    public Guid? UserId { get; init; }
}

public static class PostExtensions
{
    public static PostDto AsDto(this Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Content = post.Content,
            User = post.User,
            CreatedDate = post.CreatedDate
        };
    }
}