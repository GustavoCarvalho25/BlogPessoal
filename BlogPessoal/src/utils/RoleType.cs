using System.Text.Json.Serialization;


namespace BlogPessoal.src.utils
{   
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleType
    {
        Admin,
        User
    }
}