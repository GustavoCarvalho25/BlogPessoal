using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo post</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NewPostDTO
    {
        [Required]  // not null
        [StringLength(30)] // limitacao de tamanho
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        [StringLength(100)]
        public string CreatorEmail { get; set; }

        [Required]
        public string DescriptionTheme { get; set; }
        public NewPostDTO(string title, string description, string image, string creatorEmail, string descriptionTheme)
        {
            Title = title;
            Description = description;
            Image = image;
            CreatorEmail = creatorEmail;
            DescriptionTheme = descriptionTheme;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para alterar um post</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdatePostDTO
    {
        [Required]  // not null
        [StringLength(30)] // limitacao de tamanho
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        public string DescriptionTheme { get; set; }

        public UpdatePostDTO(string title, string description, string image, string descriptionTheme)
        {
            Title = title;
            Description = description;
            Image = image;
            DescriptionTheme = descriptionTheme;
        }
    }
}