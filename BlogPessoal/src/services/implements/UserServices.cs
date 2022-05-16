using System.Security.Claims;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace BlogPessoal.src.services.implements
{
    public class UserServices : IUserServices
    {

        #region Attributes

        private readonly IUserRepository _userRepository;
        public IConfiguration Configuration { get; }

        #endregion Attributes

        #region Constructors

        public UserServices(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            Configuration = configuration;
        }

        #endregion Constructors

        #region Methods
        public async Task CreateUserNotDuplicatedAsync(NewUserDTO dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);

            if(user != null)
                throw new Exception("Este email já está sendo utilizado");

            dto.Password = EncryptPassword(dto.Password);

            await _userRepository.NewUserAsync(dto);
        }

        public string EncryptPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        // Gerar o token, poderia estar em outra classe.
        public string GenToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, user.Email.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )

            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<AuthDTO> GetAuthorizationAsync(UserLoginDTO login)
        {
            var user = await _userRepository.GetUserByEmailAsync(login.Email);

            if(user == null)
                throw new Exception("Usuário não encontrado");

            if(user.Password != EncryptPassword(login.Password))
                throw new Exception("Senha incorreta");

            return new AuthDTO(user.Id, user.Email, user.Role, GenToken(user));
        }
        #endregion Methods
    }
}