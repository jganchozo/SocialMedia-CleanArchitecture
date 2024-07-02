using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var posts = await _postRepository.GetPosts();
        return Ok(posts);
    }
}