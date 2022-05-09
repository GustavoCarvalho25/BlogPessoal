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

        private readonly IPost _repository;

        #endregion
        
        
        #region Constructors
        
        public PostController(IPost repository)
        {
            _repository = repository;
        }

        #endregion
        
        
        #region Métodos

        [HttpGet("id/{idPost}")]
        public IActionResult GetPostById([FromRoute] int idPost)
        {
            var post = _repository.GetPostById(idPost);
            
            if (post == null) return NotFound();
            
            return Ok(post);
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var listPosts = _repository.GetAllPosts ();

            if (listPosts.Count < 1) return NoContent();
            
            return Ok(listPosts);
        }

        [HttpGet("search")]
        public IActionResult GetPostsBySearch(
            [FromQuery] string title,
            [FromQuery] string themeDescription,
            [FromQuery] string creatorName)
        {
            var posts = _repository.GetPostsBySearch(title, themeDescription, creatorName);
            
            if (posts.Count < 1) return NoContent();
            
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult NewPost([FromBody] NewPostDTO post)
        {
            if(!ModelState.IsValid) return BadRequest();

            _repository.NewPost(post);
            
            return Created($"api/Posts", post);
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] UpdatePostDTO post)
        {
            if(!ModelState.IsValid) return BadRequest();

            _repository.UpdatePost(post);
            
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();
        }
        #endregion
    }
}