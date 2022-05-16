using System.Runtime.InteropServices;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositories.implements
{
    /// <summary>
    /// <para>Resumo: Responsavel por implementar as os metodos da interface Post</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 03/05/2022</para>
    /// </summary>
    public class PostRepository : IPostRepository
    {
        #region Attributes
        private readonly PersonalBlogContext _context;
        #endregion Attributes

        #region Constructors
        public PostRepository(PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion Constructors

        #region Methods
        public async Task NewPostAsync(NewPostDTO post)
        {
            await _context.Posts.AddAsync(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Image = post.Image,
                Creator = await _context.Users.FirstOrDefaultAsync(u => u.Email == post.CreatorEmail),
                Theme = await _context.Themes.FirstOrDefaultAsync(t => t.Description == post.DescriptionTheme)
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(UpdatePostDTO post)
        {
            var _post = await GetPostByIdAsync(post.Id);
            _post.Title = post.Title;
            _post.Description = post.Description;
            _post.Image = post.Image;
            _post.Theme.Description = post.DescriptionTheme;
            _context.Posts.Update(_post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            _context.Posts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<PostModel>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PostModel>> GetPostsBySearchAsync(string title, string themeDescription, string creatorName)
        {
            switch (title, themeDescription, creatorName)
            {
                case (null, null, null):
                    return await GetAllPostsAsync();

                case (null, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(creatorName))
                    .ToListAsync();

                case (null, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(themeDescription))
                    .ToListAsync();

                case (_, null, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToListAsync();

                case (_, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) & p.Theme.Description.Contains(themeDescription))
                    .ToListAsync();

                case (null, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(themeDescription) & p.Creator.Name.Contains(creatorName))
                    .ToListAsync();
                case (_, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) & p.Creator.Name.Contains(creatorName))
                    .ToListAsync();

                case (_, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) | p.Theme.Description.Contains(themeDescription) | p.Creator.Name.Contains(creatorName))
                    .ToListAsync();
            }
        }
    }
    #endregion Methods
}
