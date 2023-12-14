using Microsoft.AspNetCore.Mvc;
using BlogApi.Models;
using BlogApi.Repositories;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IPostRepository _repository;

    public PostController(IPostRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<PostDto>> GetAll()
    {
        return (await _repository.GetPostsAsync())
            .Select(s => s.AsDto());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto>> GetById(Guid id)
    {
        var user = await _repository.GetPostAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user.AsDto();
    }

    [HttpPost]
    public async Task<ActionResult<PostDto>> Create(CreatePostDto postDto)
    {
        var post = await _repository.CreatePostAsync(postDto);

        return CreatedAtAction(nameof(GetById), new { id = post.Id }, post.AsDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PostDto>> Update(Guid id, UpdatePostDto postDto)
    {
        var post = await _repository.UpdatePostAsync(id, postDto);

        if (post is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<PostDto>> Delete(Guid id)
    {
        var post = await _repository.DeletePostAsync(id);

        if (post is null)
        {
            return NotFound();
        }

        return NoContent();
    }
}