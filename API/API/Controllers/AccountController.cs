using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }
        [HttpGet("PegarUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> PegarUsuario()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.PegarUsuarioPorUsuarioAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar encontrar o usuário. Erro: {ex.Message}");
            }
        }
        [HttpPost("RegistrarUsuario")]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarUsuario(UserDto userDto)
        {
            try
            {
                if (await _accountService.UsuarioExiste(userDto.UserName))
                {
                    return BadRequest("Usuário já existe na base de dados");
                }
                var user = await _accountService.CriarContaAsync(userDto);
                if (user != null)
                {
                    return Ok(new
                    {
                        userName = user.UserName,
                        primeiroNome = user.PrimeiroNome,
                        token = _tokenService.CreateToken(user).Result
                    });
                }
                return BadRequest("usuário não criado, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar registrar o usuário. Erro: {ex.Message}");
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            try
            {
                string msgLoginErro = "Usuário ou senha estão inválidos";
                var user = await _accountService.PegarUsuarioPorUsuarioAsync(userLoginDto.Usuario);
                if (user == null)
                {
                    return Unauthorized(msgLoginErro);
                }
                var result = await _accountService.ChecarUsuarioSenhaAsync(user, userLoginDto.Senha);
                if (!result.Succeeded)
                {
                    return Unauthorized(msgLoginErro);
                }
                return Ok(new
                {
                    userName = user.UserName,
                    primeiroNome = user.PrimeiroNome,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar realizar login. Erro: {ex.Message}");
            }
        }
        [HttpPut("AlterarUsuario")]
        public async Task<IActionResult> AlterarUsuario(UserUpdateDto userUpdateDto)
        {
            try
            {
                if (userUpdateDto.UserName != User.GetUserName())
                {
                    return Unauthorized("Usuário invalido");
                }
                var user = await _accountService.PegarUsuarioPorUsuarioAsync(User.GetUserName());
                if (user == null)
                {
                    return Unauthorized("Usuário inválido");
                }

                var userReturn = await _accountService.AlterarContaAsync(userUpdateDto);
                if (userReturn != null)
                {
                    return Ok(new
                    {
                        userName = userReturn.UserName,
                        primeiroNome = userReturn.PrimeiroNome,
                        token = _tokenService.CreateToken(user).Result
                    });
                }
                return BadRequest("usuário não criado, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar atualizar o usuário. Erro: {ex.Message}");
            }
        }
    }
}
