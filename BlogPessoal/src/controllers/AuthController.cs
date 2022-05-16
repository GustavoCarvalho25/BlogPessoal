using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        #region Attributes

        private readonly IUserServices _services;

        #endregion Attributes
        
        #region Constructors

        public AuthController(IUserServices services)
        {
            _services = services;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserLoginDTO auth)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            
            try
            {
                var authorization = await _services.GetAuthorizationAsync(auth);
                return Ok(authorization);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion Methods
    }
}