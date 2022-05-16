using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
using BlogPessoal.src.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Tests.repositories
{   
    [TestClass]
    public class UserRepositoryTest
    {
        private PersonalBlogContext _context;
        private IUserRepository _repository;

        [TestMethod]
        public async Task CriarCincoUsuariosNoBancoRetornaQuatroUsuarios()
        {
            var options = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_BlogPessoal1")
                .Options;
            _context = new PersonalBlogContext(options);
            _repository = new UserRepository(_context);

            // Given - Dado que registro 5 usuarios no banco

            await _repository.NewUserAsync(
                new NewUserDTO(
                    "Gustavo Carvalho",
                    "gustavo@email.com",
                    "12345",
                    "Linkimage",
                    RoleType.User
                )
            );

            await _repository.NewUserAsync(
                new NewUserDTO(
                    "Maria Carvalho",
                    "maria@email.com",
                    "12345",
                    "Linkimage",
                    RoleType.User
                )
            );

            await _repository.NewUserAsync(
                new NewUserDTO(
                    "Joao Carvalho",
                    "Joao@email.com",
                    "12345",
                    "Linkimage",
                    RoleType.User
                )
            );

            await _repository.NewUserAsync(
                new NewUserDTO(
                    "Enzo Carvalho",
                    "Enzo@email.com",
                    "12345",
                    "Linkimage",
                    RoleType.User
                )
            );

            await _repository.NewUserAsync(
                new NewUserDTO(
                    "Guilherme Carvalho",
                    "guilherme@email.com",
                    "12345",
                    "Linkimage",
                    RoleType.User
                )
            );
            // WHEN - Quando pesquiso lista total
            // THEN - Então recebo 5 usuarios
            Assert.AreEqual(5, _context.Users.Count());
        }

        [TestMethod]
        public async Task PegarUsuarioPeloEmailRetornaNaoNulo()
        {   
            var options = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_BlogPessoal2")
                .Options;
            _context = new PersonalBlogContext(options);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
                new NewUserDTO(
                    "Gustavo Boaz",
                    "boaz@email.com",
                    "12345",
                    "linkimage", 
                    RoleType.User
                )
            );

            // WHEN -Quando pesquiso pelo email deste usuario
            var user = await _repository.GetUserByEmailAsync("boaz@email.com");

            // THEN - Entao obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
            // Definindo o contexto
            var options = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_BlogPessoal3")
                .Options;

            _context = new PersonalBlogContext(options);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
                new NewUserDTO("Neusa Boaz","neusa@email.com","134652","URLFOTO", RoleType.User)
            );
            
			//WHEN - Quando pesquiso pelo id 1
            var user = await _repository.GetUserByIdAsync(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }

        [TestMethod]
        public async Task AtualizarUsuarioRetornaUsuarioAtualizado()
        {   
            var options = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_BlogPessoal4")
                .Options;
            _context = new PersonalBlogContext(options);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
            new NewUserDTO(
            "Estefânia Boaz",
            "estefania@email.com",
            "134652",
            "URLFOTO",
            RoleType.User));
            //WHEN - Quando atualizamos o usuario
            var antigo = await _repository.GetUserByEmailAsync("estefania@email.com");

            await _repository.UpdateUserAsync(
            new UpdateUserDTO(
            1,
            "Estefânia Moura",
            "123456",
            "URLFOTONOVA",
            "estefania@email.com"));
            //THEN - Então, quando validamos pesquisa deve
            //retornar nome Estefânia Moura
            Assert.AreEqual(
                "Estefânia Moura",  _context.Users.FirstOrDefault(u => u.Id == antigo.Id).Name);

            Assert.AreEqual(
                "123456",  _context.Users.FirstOrDefault(u => u.Id == antigo.Id).Password);
        }
    }
}