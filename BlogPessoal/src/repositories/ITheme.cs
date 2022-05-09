using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.repositories
{   
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações CRUD de theme</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface ITheme
    {
        void NewTheme(NewThemeDTO theme);
        void UpdateTheme(UpdateThemeDTO theme);
        void DeleteTheme(int id);
        ThemeModel GetThemeById(int id);
        List<ThemeModel> GetAllThemes();
        List<ThemeModel> GetThemeByDescription(string description);
    }
}