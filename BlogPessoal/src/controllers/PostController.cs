using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Attibutes

        private readonly IPostRepository _repository;

        #endregion
        
        
        #region Constructors
        
        public PostController(IPostRepository repository)
        {
            _repository = repository;
        }

        #endregion
        
        
        #region Métodos

        [HttpGet("id/{idPost}")]
        public async Task<IActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);
            
            if (post == null) return NotFound();
            
            return Ok(post);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var listPosts = await _repository.GetAllPostsAsync ();

            if (listPosts.Count() < 1) return NoContent();
            
            return Ok(listPosts);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetPostsBySearchAsync(
            [FromQuery] string title,
            [FromQuery] string themeDescription,
            [FromQuery] string creatorName)
        {
            var posts = await _repository.GetPostsBySearchAsync(title, themeDescription, creatorName);
            
            if (posts.Count() < 1) return NoContent();
            
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> NewPostAsync([FromBody] NewPostDTO post)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _repository.NewPostAsync(post);
            
            return Created($"api/Posts", post);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePostAsync([FromBody] UpdatePostDTO post)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _repository.UpdatePostAsync(post);
            
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        public async Task<IActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();
        }
        #endregion
    }
}