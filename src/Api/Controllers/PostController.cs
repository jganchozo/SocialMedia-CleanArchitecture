using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostController(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _postRepository.GetPosts();
        var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

        return Ok(postsDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPost(int id)
    {
        var post = await _postRepository.GetPost(id);

        if (post is null) return NotFound(post);
        
        var postDto = _mapper.Map<PostDto>(post);
        
        return Ok(postDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(PostDto postDto)
    {
        var post = _mapper.Map<Post>(postDto);
        
        await _postRepository.InsertPost(post);
        
        return Ok(post);
    }
}
