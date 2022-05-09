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
    [Route("api/Themes")]
    [Produces("application/json")]

    public class ThemeController : ControllerBase
    {
        #region Attributes

        private readonly ITheme _repository;

        #endregion 


        #region Constructors

        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }

        #endregion 


        #region Methods

        [HttpGet]
        public IActionResult GetAllThemes()
        {
            var listThemes = _repository.GetAllThemes();

            if (listThemes.Count < 1) return NoContent();
            
            return Ok(listThemes);
        }

        [HttpGet("id/{idTheme}")]
        public IActionResult GetThemeById([FromRoute] int idTheme)
        {
            var theme = _repository.GetThemeById(idTheme);

            if (theme == null) return NotFound();
            
            return Ok(theme);
        }

        [HttpGet("search")]
        public IActionResult GetThemeByDescription([FromQuery] string themeDescription)
        {
            var themes = _repository.GetThemeByDescription(themeDescription);

            if (themes.Count < 1) return NoContent();
            
            return Ok(themes);
        }

        [HttpPost]
        public IActionResult NewTheme([FromBody] NewThemeDTO theme)
        {
            if(!ModelState.IsValid) return BadRequest();

            _repository.NewTheme(theme);
            
            return Created($"api/Themes", theme);
        }

        [HttpPut]
        public IActionResult UpdateTheme([FromBody] UpdateThemeDTO theme)
        {
            if(!ModelState.IsValid) return BadRequest();

            _repository.UpdateTheme(theme);
            
            return Ok(theme);
        }

        [HttpDelete("delete/{idTheme}")]
        public IActionResult DeleteTheme([FromRoute] int idTheme)
        {
            _repository.DeleteTheme(idTheme);
            return NoContent();
        }

        #endregion
    }
}