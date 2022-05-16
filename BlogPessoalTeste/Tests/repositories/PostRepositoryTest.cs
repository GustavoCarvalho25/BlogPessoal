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
    public class PostRepositoryTest
    {
        private PersonalBlogContext _context;
        private IUserRepository _repositoryU;
        private IThemeRepository _repositoryT;
        private IPostRepository _repositoryP;

        [TestMethod]
        public async Task CreateThreePostsInSystemReturnThree()
        {
            // Definindo o contexto
            var options = new DbContextOptionsBuilder<PersonalBlogContext>()
                 .UseInMemoryDatabase(databaseName: "db_BlogPessoal6")
                 .Options;

            _context = new PersonalBlogContext(options);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 2 usuarios
            await _repositoryU.NewUserAsync(
                new NewUserDTO
                ("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", RoleType.User)
            );

            await _repositoryU.NewUserAsync(
                new NewUserDTO
                ("Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO", RoleType.User)
            );

            // AND - E que registro 2 temas
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            // WHEN - Quando eu busco todas as postagens
            var themes = await _repositoryP.GetAllPostsAsync();

            // THEN - Eu tenho 3 postagens
            Assert.AreEqual(3, themes.Count);
        }

        [TestMethod]
        public async Task UpdatePostReturnPostActualized()
        {
            // Definindo o contexto
            var options = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_BlogPessoal7")
                .Options;

            _context = new PersonalBlogContext(options);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 1 usuarios
            await _repositoryU.NewUserAsync(
                new NewUserDTO
                ("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", RoleType.User)
            );

            // AND - E que registro 1 tema
            await _repositoryT.NewThemeAsync(new NewThemeDTO("COBOL"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));

            // AND - E que registro 1 postagem
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "COBOL é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "COBOL"
                )
            );

            // WHEN - Quando atualizo postagem de id 1
            await _repositoryP.UpdatePostAsync(
                new UpdatePostDTO(
                    1,
                    "C# é muito massa",
                    "C# é muito utilizada no mundo",
                    "URLDAFOTOATUALIZADA",
                    "C#"
                )
            );

            // THEN - Eu tenho a postagem atualizada
            Assert.AreEqual(
                "C# é muito massa",
                (await _repositoryP.GetPostByIdAsync(1)).Title
            );
            Assert.AreEqual(
                "C# é muito utilizada no mundo",
                (await _repositoryP.GetPostByIdAsync(1)).Description
            );
            Assert.AreEqual(
                "URLDAFOTOATUALIZADA",
                (await _repositoryP.GetPostByIdAsync(1)).Image
            );
            Assert.AreEqual(
                "C#",
                (await _repositoryP.GetPostByIdAsync(1)).Theme.Description
            );
        }

        [TestMethod]
        public async Task GetPostBySearchReturnCustom()
        {
            // Definindo o contexto
            var options = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_BlogPessoal8")
                .Options;

            _context = new PersonalBlogContext(options);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 2 usuarios
            await _repositoryU.NewUserAsync(
                new NewUserDTO
                ("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO", RoleType.User)
            );

            await _repositoryU.NewUserAsync(
                new NewUserDTO
                ("Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO", RoleType.User)
            );

            // AND - E que registro 2 temas
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            // WHEN - Quando eu busco as postagen
            // THEN - Eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(
                2,
                (await _repositoryP.GetPostsBySearchAsync("massa", null, null)).Count
            );
            Assert.AreEqual(
                2,
                (await _repositoryP.GetPostsBySearchAsync(null, "C#", null)).Count
            );
            Assert.AreEqual(
                2,
                (await _repositoryP.GetPostsBySearchAsync(null, null, "Gu")).Count
            );
        }
    }
}   