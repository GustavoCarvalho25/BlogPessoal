using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;

namespace BlogPessoal.src.repositories.implements
{   
    /// <summary>
    /// <para>Resumo: Responsavel por implementar as os metodos da interface Theme </para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 03/05/2022</para>
    /// </summary>
    public class ThemeRepository : ITheme
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
        public void NewTheme(NewThemeDTO theme)
        {
            _context.Themes.Add(new ThemeModel{
                Description = theme.Description
            });
            _context.SaveChanges();
        }

        public void UpdateTheme(UpdateThemeDTO theme)
        {
            var _theme = GetThemeById(theme.Id);
            _theme.Description = theme.Description;
            _context.Themes.Update(_theme);
            _context.SaveChanges();
        }

        public void DeleteTheme(int id)
        {
            _context.Themes.Remove(GetThemeById(id));
            _context.SaveChanges();
        }

        public List<ThemeModel> GetThemeByDescription(string description)
        {
            return _context.Themes
            .Where(t => t.Description.Contains(description))
            .ToList();
        }

        public ThemeModel GetThemeById(int id)
        {
            return _context.Themes.FirstOrDefault(t => t.Id == id);
        }

        public List<ThemeModel> GetAllThemes()
        {
            return _context.Themes.ToList();
        }
        #endregion Methods
    }
}