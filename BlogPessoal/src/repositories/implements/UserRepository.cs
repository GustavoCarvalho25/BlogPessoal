using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositories.implements
{   
    /// <summary>
    /// <para>Resumo: Responsavel por implementar as os metodos da interface User </para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 02/05/2022</para>
    /// </summary> 
    public class UserRepository : IUserRepository
    {   
        #region Attributes
        private readonly PersonalBlogContext _context;
        #endregion Attributes

        #region Constructors
        public UserRepository(PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion Constructors

        #region Methods

        /// <summary>
        /// <para>Resumo: Metodo para criar um usuario</para>
        /// </summary>
        /// <param name="user">NewUserDTO</param>
        public async Task NewUserAsync(NewUserDTO user)
        {
            await _context.Users.AddAsync(new UserModel {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Image = user.Image,
                Role = user.Role
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Metodo para atualizar um usuario</para>
        /// </summary>
        /// <param name="user">UpdateUserDTO</param>
        public async Task UpdateUserAsync(UpdateUserDTO user)
        {
            var _user = await GetUserByIdAsync(user.Id);
            _user.Name = user.Name;
            _user.Password = user.Password;
            _user.Image = user.Image;
            _context.Users.Update(_user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Metodo para deletar um usuario</para>
        /// </summary>
        /// <param name="id">int</param>
        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Metodo para obter um usuario pelo id</para>
        /// </summary>
        /// <param name="id">int</param>
        /// <return>UserModel</return>
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Metodo para obter um usuario pelo email</para>
        /// </summary>
        /// <param name="email">String</param>
        /// <return>UserModel</return>
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resumo: Metodo para obter uma lista de usuarios pelo nome</para>
        /// </summary>
        /// <param name="name">String</param>
        /// <returns>UserModel</returns>
        public async Task<List<UserModel>> GetUserByNameAsync(string name)
        {
            return await _context.Users
            .Where(u => u.Name.Contains(name))
            .ToListAsync();
        }
        #endregion Methods
    }
}