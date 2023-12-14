using Microsoft.AspNetCore.Mvc;
using BlogApi.Models;
using BlogApi.Repositories;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IPostRepository _postRepository;

    public UserController(IUserRepository repository, IPostRepository postRepository)
    {
        _repository = repository;
        _postRepository = postRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> GetAll()
    {
        return (await _repository.GetUsersAsync())
            .Select(user => user.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        var user = await _repository.GetUserAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto userDto)
    {
        var user = await _repository.CreateUserAsync(userDto);

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> Update(Guid id, UpdateUserDto userDto)
    {
        var user = await _repository.UpdateUserAsync(id, userDto);

        if (user is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDto>> Delete(Guid id)
    {
        var user = await _repository.DeleteUserAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}/posts")]
    public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByUserId(Guid id)
    {
        var user = await _repository.GetUserAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        var posts = await _postRepository.GetPostsByUserIdAsync(id);

        return Ok(posts.Select(p => p.AsDto()));
    }
}