using System.ComponentModel.DataAnnotations;

namespace BlogApi.Models;

public record User
{
    [Key]
    public Guid Id { get; init; }

    [Required]
    [MaxLength(20)]
    public string? Username { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Email { get; init; }

    [Required]
    public string? Password { get; init; }

    public DateTimeOffset CreatedDate { get; init; }

    public IList<Post> Posts { get; } = new List<Post>();
}

public record UserDto
{
    public Guid Id { get; init; }
    public string? Username { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
    public ICollection<PostDto> Posts { get; init; } = new List<PostDto>();
}

public record CreateUserDto
{
    [Required]
    [MaxLength(20)]
    public string? Username { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Email { get; init; }

    [Required]
    public string? Password { get; init; }
}

public record UpdateUserDto
{
    [Required]
    [MaxLength(20)]
    public string? Username { get; init; }

    [Required]
    [MaxLength(50)]
    public string? Email { get; init; }

    [Required]
    public string? Password { get; init; }
}

public static class UserExtensions
{
    public static UserDto AsDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            CreatedDate = user.CreatedDate,
            Posts = user.Posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedDate = p.CreatedDate
            }).ToList()
        };
    }
}