using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositories.implements
{   
    /// <summary>
    /// <para>Resumo: Responsavel por implementar as os metodos da interface Theme </para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 03/05/2022</para>
    /// </summary>
    public class ThemeRepository : IThemeRepository
    {   
        #region Attributes
        private readonly PersonalBlogContext _context;
        #endregion Attributes

        #region Constructors
        public ThemeRepository (PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// <para>Resumo: Método responsavel por criar um novo tema</para>
        /// </summary>
        /// <param name="theme">NewThemeDTO</param>
        public async Task NewThemeAsync(NewThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel{
                Description = theme.Description
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método responsavel por atualizar um tema</para>
        /// </summary>
        /// <param name="theme">UpdateThemeDTO</param>
        public async Task UpdateThemeAsync(UpdateThemeDTO theme)
        {
            var _theme = await GetThemeByIdAsync(theme.Id);
            _theme.Description = theme.Description;
            _context.Themes.Update(_theme);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método responsavel por deletar um tema</para>
        /// </summary>
        /// <param name="id">int</param>
        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método responsavel por retornar temas pela descrição</para>
        /// </summary>
        /// <param name="description">string</param>
        /// <returns>Lista ThemeModel</returns>
        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes
            .Where(t => t.Description.Contains(description))
            .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Método responsavel por retornar um tema pelo id</para>
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>Lista ThemeModel</returns>
        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Método responsavel por retornar todos os temas</para>
        /// </summary>
        /// <returns>Lista ThemeModel</returns>
        public async Task<List<ThemeModel>> GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }
        #endregion Methods
    }
}