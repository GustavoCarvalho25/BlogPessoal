using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo tema</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NewThemeDTO
    {
        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        public NewThemeDTO(string description)
        {
            Description = description;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para alterar um tema</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdateThemeDTO
    {   
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Description { get; set; }

        public UpdateThemeDTO(int id, string description)
        {   
            Id = id;
            Description = description;
        }
    }
}