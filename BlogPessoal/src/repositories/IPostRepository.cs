using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.repositories
{   
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações CRUD de post</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IPostRepository
    {
        Task NewPostAsync(NewPostDTO post);
        Task UpdatePostAsync(UpdatePostDTO post);
        Task DeletePostAsync(int id);
        Task<PostModel> GetPostByIdAsync(int id);
        Task<List<PostModel>> GetAllPostsAsync();
        Task<List<PostModel>> GetPostsBySearchAsync (string title, string themeDescription, string creatorName);
    }
}