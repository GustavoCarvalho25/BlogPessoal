using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{   
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attributes

        private readonly IUser _repository;

        #endregion 


        #region Constructors

        public UserController(IUser repository)
        {
            _repository = repository;
        }

        #endregion 


        #region Methods

        [HttpGet("id/{idUser}")]
        public IActionResult GetUserById([FromRoute] int idUser) // vem da rota inteira
        {
            var user = _repository.GetUserById(idUser);

            if(user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUserByName([FromQuery] string nameUser) //vem de um atributo da rota
        {
            var users = _repository.GetUserByName(nameUser);

            if(users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser) // vem da rota inteira
        {
            var user = _repository.GetUserByEmail(emailUser);

            if(user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult NewUser([FromBody] NewUserDTO user)
        {
            if(!ModelState.IsValid) return BadRequest();

            return Created($"api/Users/email/{user.Email}", user);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if(!ModelState.IsValid) return BadRequest();
            _repository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        public IActionResult DeleteUser([FromBody] int idUser)
        {
            
            _repository.DeleteUser(idUser);
            return NoContent();
        }
        #endregion
    }
}