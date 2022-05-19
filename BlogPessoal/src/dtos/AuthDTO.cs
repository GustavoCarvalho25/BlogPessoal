using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.utils;

namespace BlogPessoal.src.dtos
{   
    /// <summary>
    /// <para>Resumo: Classe espelho para autenticar um usuario</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class UserLoginDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public UserLoginDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para representar a autorização de um usuario</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class AuthDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
        public string Token { get; set; }

        public AuthDTO(int id, string email, RoleType role, string token)
        {
            Id = id;
            Email = email;
            Role = role;
            Token = token;
        }
    }
}