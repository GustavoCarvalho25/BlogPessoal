using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Testes.Data
{   
    [TestClass]
    public class PersonalBlogContextTest
    {
        private PersonalBlogContext _context;

        [TestInitialize]
        public void Inicio()
        {
            var options = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_BlogPessoal")
            .Options;

            _context = new PersonalBlogContext(options);
        }

        [TestMethod]
        public void InserirNovoUsuarioNoBancoDeDadosRetornarUsuario()
        {
            UserModel user = new UserModel();

            user.Name = "Gustavo Carvalho";
            user.Email = "gustavo@email.com";
            user.Password = "1234567890";
            user.Image = "linkImage";

            _context.Users.Add(user);

            _context.SaveChanges();

            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Email == "gustavo@email.com"));
        }
    }
}