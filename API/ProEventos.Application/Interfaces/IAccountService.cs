using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> UsuarioExiste(string usuario);
        Task<UserUpdateDto> PegarUsuarioPorUsuarioAsync(string usuario);
        Task<SignInResult> ChecarUsuarioSenhaAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserUpdateDto> CriarContaAsync(UserDto userDto);
        Task<UserUpdateDto> AlterarContaAsync(UserUpdateDto userUpdateDto);
    }
}
