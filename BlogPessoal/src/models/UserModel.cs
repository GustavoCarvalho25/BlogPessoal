using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlogPessoal.src.utils;

namespace BlogPessoal.src.models
{   
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_User no banco</para>
    /// <para>Criado por: Gustavo Carvalho</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    [Table("tb_Users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        [Required]
        public RoleType Role { get; set; }

        [JsonIgnore] // json ignora, a visualizacao e pela postmodel
        public List<PostModel> MyPosts { get; set; }
    }
}