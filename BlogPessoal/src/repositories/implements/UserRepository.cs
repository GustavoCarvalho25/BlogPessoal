using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories.implementations;

namespace BlogPessoal.src.repositories.implements
{
    public class UserRepository : IUser
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
        public void NewUser(NewUserDTO user)
        {
            _context.Users.Add(new UserModel {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Image = user.Image
            });
            _context.SaveChanges();

        }

        public void UpdateUser(UpdateUserDTO user)
        {
            var _user = GetUserById(user.Id);
            _user.Name = user.Name;
            _user.Email = user.Email;
            _user.Password = user.Password;
            _user.Image = user.Image;
            _context.Update(_user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _context.Users.Remove(GetUserById(id));
            _context.SaveChanges();
        }

        public UserModel GetUserById(int id)
        {
            return _context.Users
            .FirstOrDefault(u => u.Id == id);
        }
        public UserModel GetUserByEmail(string email)
        {
            return _context.Users
            .FirstOrDefault(u => u.Email == email);
        }
        public List<UserModel> GetUserByName(string name)
        {
            return _context.Users
            .Where(u => u.Name.Contains(name))
            .ToList();
        }
        #endregion Methods
    }
}