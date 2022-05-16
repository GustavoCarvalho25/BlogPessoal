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
    public interface IThemeRepository
    {
        Task NewThemeAsync(NewThemeDTO theme);
        Task UpdateThemeAsync(UpdateThemeDTO theme);
        Task DeleteThemeAsync(int id);
        Task <ThemeModel> GetThemeByIdAsync(int id);
        Task <List<ThemeModel>> GetAllThemesAsync();
        Task <List<ThemeModel>> GetThemeByDescriptionAsync(string description);
    }
}