using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly IMapper _mapper;
        private readonly IUserPersist _userPersist;

        public AccountService(UserManager<User> userManager, SignInManager<User> signManager, IMapper mapper, IUserPersist userPersist)
        {
            _userManager = userManager;
            _signManager = signManager;
            _mapper = mapper;
            _userPersist = userPersist;
        }

        public async Task<UserUpdateDto> AlterarContaAsync(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _userPersist.PegarUsuarioPorNomeAsync(userUpdateDto.UserName);
                if (user == null)
                {
                    return null;
                }

                userUpdateDto.Id = user.Id;
                _mapper.Map(userUpdateDto, user);

                if (userUpdateDto.Password != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);
                }

                _userPersist.Update(user);

                if (await _userPersist.SaveChangesAsync())
                {
                    var userRetorno = await _userPersist.PegarUsuarioPorNomeAsync(user.UserName);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao tentar alterar um usuário. Erro: {e.Message}");
            }
        }

        public async Task<SignInResult> ChecarUsuarioSenhaAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(user => user.NormalizedUserName == userUpdateDto.UserName.ToUpper());
                return await _signManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao tentar verificar um usuário. Erro: {e.Message}");
            }
        }

        public async Task<UserUpdateDto> CriarContaAsync(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserUpdateDto>(user);
                    return userToReturn;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao tentar criar um usuário. Erro: {e.Message}");
            }
        }

        public async Task<UserUpdateDto> PegarUsuarioPorUsuarioAsync(string usuario)
        {
            try
            {
                var user = await _userPersist.PegarUsuarioPorNomeAsync(usuario);
                if (user == null)
                {
                    return null;
                }
                var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
                return userUpdateDto;
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao tentar encontrar um usuário. Erro: {e.Message}");
            }
        }

        public async Task<bool> UsuarioExiste(string usuario)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.UserName == usuario.ToLower());
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao verificar se o usuário existe. Erro: {e.Message}");
            }
        }
    }
}
