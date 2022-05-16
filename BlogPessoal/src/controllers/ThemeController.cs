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

        private readonly IThemeRepository _repository;

        #endregion 


        #region Constructors

        public ThemeController(IThemeRepository repository)
        {
            _repository = repository;
        }

        #endregion 


        #region Methods

        [HttpGet]
        public async Task<IActionResult> GetAllThemesAsync()
        {
            var listThemes = await _repository.GetAllThemesAsync();

            if (listThemes.Count() < 1) return NoContent();
            
            return Ok(listThemes);
        }

        [HttpGet("id/{idTheme}")]
        public async Task<IActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idTheme);

            if (theme == null) return NotFound();
            
            return Ok(theme);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetThemeByDescriptionAsync([FromQuery] string themeDescription)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(themeDescription);

            if (themes.Count() < 1) return NoContent();
            
            return Ok(themes);
        }

        [HttpPost]
        public async Task<IActionResult> NewThemeAsync([FromBody] NewThemeDTO theme)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _repository.NewThemeAsync(theme);
            
            return Created($"api/Themes", theme);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateThemeAsync([FromBody] UpdateThemeDTO theme)
        {
            if(!ModelState.IsValid) return BadRequest();

            await _repository.UpdateThemeAsync(theme);
            
            return Ok(theme);
        }

        [HttpDelete("delete/{idTheme}")]
        public async Task<IActionResult> DeleteThemeAsync([FromRoute] int idTheme)
        {
            await _repository.DeleteThemeAsync(idTheme);
            return NoContent();
        }

        #endregion
    }
}