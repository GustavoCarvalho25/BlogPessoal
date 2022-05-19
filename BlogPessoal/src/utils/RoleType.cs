using System.Text.Json.Serialization;


namespace BlogPessoal.src.utils
{   

    /// <summary>
    /// <para> Resumo: Enum responsavel por definir Tipos de usuario no sistema</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleType
    {
        Admin,
        User
    }
}