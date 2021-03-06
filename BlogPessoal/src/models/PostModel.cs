using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.models
{   
     /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_Post no banco</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    [Table("tb_Posts")]
    public class PostModel
    {
        [Key] //chave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //id automatico
        public int Id { get; set; }

        [Required]  // not null
        [StringLength(30)] // limitacao de tamanho
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Required]
        [ForeignKey("fk_Users")] // chave estrangeira
        public UserModel Creator { get; set; }
        
        [Required]
        [ForeignKey("fk_Themes")]
        public ThemeModel Theme { get; set; }

    }
}