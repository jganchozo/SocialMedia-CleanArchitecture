using Api.Responses;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;

    public PostController(IMapper mapper, IPostService postService)
    {
        _postService = postService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
    public IActionResult GetPosts([FromQuery] PostQueryFilter filters)
    {
        var posts = _postService.GetPosts(filters);
        var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
        var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPost(int id)
    {
        var post = await _postService.GetPost(id);

        if (post is null) return NotFound(post);
        
        var postDto = _mapper.Map<PostDto>(post);
        var response = new ApiResponse<PostDto>(postDto);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Post(PostDto postDto)
    {
        var post = _mapper.Map<Post>(postDto);

        await _postService.InsertPost(post);
        
        postDto = _mapper.Map<PostDto>(post);
        var response = new ApiResponse<PostDto>(postDto);
        return Ok(response);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(int id, PostDto postDto)
    {
        var post = _mapper.Map<Post>(postDto);
        post.Id = id;

        var result = await _postService.UpdatePost(post);
        var response = new ApiResponse<bool>(result);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool result = await _postService.DeletePost(id);
        var response = new ApiResponse<bool>(result);
        return Ok(response);
    }
}
