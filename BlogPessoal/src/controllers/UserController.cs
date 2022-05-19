using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{   
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attributes

        private readonly IUserRepository _repository;
        private readonly IUserServices _services;

        #endregion 


        #region Constructors

        public UserController(IUserRepository repository, IUserServices services)
        {
            _repository = repository;
            _services = services;
        }

        #endregion 


        #region Methods

        /// <summary>
        /// <para>Resumo: Retorna um usuario pelo id</para>
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Usuario não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUser}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] int idUser) // vem da rota inteira
        {
            var user = await _repository.GetUserByIdAsync(idUser);

            if(user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// <para>Resumo: Retorna um usuario pelo nome</para>
        /// </summary>
        /// <param name="nameUser">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="204">Nome não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<IActionResult> GetUserByNameAsync([FromQuery] string nameUser) //vem de um atributo da rota
        {
            var users = await _repository.GetUserByNameAsync(nameUser);

            if(users.Count() < 1) return NoContent();

            return Ok(users);
        }

        /// <summary>
        /// <para>Resumo: Retorna um usuario pelo email</para>
        /// </summary>
        /// <param name="emailUser">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="404">Email não existente</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("email/{emailUser}")]
        public async Task <IActionResult> GetUserByEmail([FromRoute] string emailUser) // vem da rota inteira
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);

            if(user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Criar novo Usuario
        /// </summary>
        /// <param name="user">NewUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Users
        ///     {
        ///        "name": "Gustavo Boaz",
        ///        "email": "gustavo@domain.com",
        ///        "password": "134652",
        ///        "image": "URLFOTO",
        ///        "role": "User"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna usuario criado</response>
        /// <response code="400">Erro na requisição</response>
        /// <response code="401">E-mail ja cadastrado</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> NewUserAsync([FromBody] NewUserDTO user)
        {
            if(!ModelState.IsValid) return BadRequest();

            try
            {
                await _services.CreateUserNotDuplicatedAsync(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserDTO user)
        {
            if(!ModelState.IsValid) return BadRequest();

            user.Password =  _services.EncryptPassword(user.Password);

            await _repository.UpdateUserAsync(user);
            return Ok(user);
        }

        
        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserAsync([FromBody] int idUser)
        {
            
            await _repository.DeleteUserAsync(idUser);
            return NoContent();
        }
        #endregion
    }
}