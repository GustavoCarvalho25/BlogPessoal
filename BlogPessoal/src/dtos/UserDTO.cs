using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.utils;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo usuario</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NewUserDTO
    {   
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        public string Image { get; set; }

        public RoleType Role { get; set; }

        public NewUserDTO(string name, string email, string password, string image, RoleType role)
        {
            Name = name;
            Email = email;
            Password = password;
            Image = image;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para alterar um usuario</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdateUserDTO
    {   
        [Required]
        public int Id { get; set;}

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        public string Image { get; set; }

        public UpdateUserDTO(int id, string name, string password, string image, string email)
        {   
            Id = id;
            Name = name;
            Password = password;
            Image = image;
            Email = email;
        }
    }
}