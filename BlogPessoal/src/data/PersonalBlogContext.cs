using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{   
    /// <summary>
    /// <para>Resumo: Classe contexto, responsavel por carregar contexto e definir DbSets</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class PersonalBlogContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ThemeModel> Themes { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public PersonalBlogContext(DbContextOptions<PersonalBlogContext> options) : base(options)
        {
        }
    }
}