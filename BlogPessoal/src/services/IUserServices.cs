using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.services
{   
    /// <summary>
    /// <para>Resume: Interface responsavel pela interpretacao logica de servicos do usuario</para>
    /// <para>Creado por: Gustavo Carvalho</para>
    /// <para>Versao: 1.0</para>
    /// <para>Data: 10/05/2022</para>
    /// </summary>
    public interface IUserServices
    {   
        string EncryptPassword(string password);
        Task CreateUserNotDuplicatedAsync(NewUserDTO user);
        Task<AuthDTO> GetAuthorizationAsync(UserLoginDTO login);
        string GenToken(UserModel user);
    }
}