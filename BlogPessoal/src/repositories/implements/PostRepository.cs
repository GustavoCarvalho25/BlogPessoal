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
    public class PostRepository : IPost
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
        public void NewPost(NewPostDTO post)
        {
            _context.Posts.Add(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Image = post.Image,
                Creator = _context.Users.FirstOrDefault(u => u.Email == post.CreatorEmail),
                Theme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme)
            });
            _context.SaveChanges();
        }

        public void UpdatePost(UpdatePostDTO post)
        {
            var _post = GetPostById(post.Id);
            _post.Title = post.Title;
            _post.Description = post.Description;
            _post.Image = post.Image;
            _post.Theme.Description = post.DescriptionTheme;
            _context.Posts.Update(_post);
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            _context.Posts.Remove(GetPostById(id));
            _context.SaveChanges();
        }

        public List<PostModel> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public PostModel GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public List<PostModel> GetPostsBySearch(string title, string themeDescription, string creatorName)
        {
            switch (title, themeDescription, creatorName)
            {
                case (null, null, null):
                    return GetAllPosts();

                case (null, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(creatorName))
                    .ToList();

                case (null, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(themeDescription))
                    .ToList();

                case (_, null, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToList();

                case (_, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) & p.Theme.Description.Contains(themeDescription))
                    .ToList();

                case (null, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(themeDescription) & p.Creator.Name.Contains(creatorName))
                    .ToList();
                case (_, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) & p.Creator.Name.Contains(creatorName))
                    .ToList();

                case (_, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) | p.Theme.Description.Contains(themeDescription) | p.Creator.Name.Contains(creatorName))
                    .ToList();
            }
        }
    }
    #endregion Methods
}
