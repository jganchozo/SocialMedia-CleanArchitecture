using System.Text.Json;
using Api.Responses;
using AutoMapper;
using Core.CustomEntities;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Core.QueryFilters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]s")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IMapper _mapper;
    private readonly IUriService _uriService;

    public PostController(IMapper mapper, IPostService postService, IUriService uriService)
    {
        _postService = postService;
        _mapper = mapper;
        _uriService = uriService;
    }

    /// <summary>
    /// Retrieve all posts
    /// </summary>
    /// <param name="filters">filters</param>
    /// <returns></returns>
    [HttpGet(Name = nameof(GetPosts))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<IEnumerable<PostDto>>))]
    public IActionResult GetPosts([FromQuery] PostQueryFilter filters)
    {
        var posts = _postService.GetPosts(filters);
        var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

        Metadata metadata = new()
        {
            TotalCount = posts.TotalCount,
            PageSize = posts.PageSize,
            CurrentPage = posts.CurrentPage,
            TotalPages = posts.TotalPages,
            HasPreviousPage = posts.HasPreviousPage,
            HasNextPage = posts.HasNextPage,
            NextPageUrl = _uriService.GetPostPaginationUri(filters, posts.NextPageNumber, Url.RouteUrl(nameof(GetPosts))!)?.ToString(),
            PreviousPageUrl = _uriService.GetPostPaginationUri(filters, posts.PreviousPageNumber, Url.RouteUrl(nameof(GetPosts))!)?.ToString()
        };

        var response = new ApiResponse<IEnumerable<PostDto>>(postsDto)
        {
            Meta = metadata
        };

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metadata));

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
