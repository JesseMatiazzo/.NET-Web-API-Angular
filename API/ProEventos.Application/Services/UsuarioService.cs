using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IUsuarioPersist _usuarioPersist;

        public UsuarioService(IGeralPersist geralPersist, IUsuarioPersist usuarioPersist)
        {
            _geralPersist = geralPersist;
            _usuarioPersist = usuarioPersist;
        }

        public async Task<Usuario> AddUsuario(Usuario model)
        {
            try
            {
                _geralPersist.Add(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _usuarioPersist.PegarUsuario(model.UsuarioLogin, model.Senha);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteUsuario(string usuario, string senha)
        {
            try
            {
                var evento = _usuarioPersist.PegarUsuario(usuario, senha);
                if (evento == null)
                {
                    throw new Exception("Usuario para delete não foi encontrado");
                }
                _geralPersist.Delete(evento.Result);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario[]> ListarUsuarios()
        {
            try
            {
                var dados = await _usuarioPersist.ListarUsuarios();
                if (dados == null)
                {
                    return null;
                }
                return dados;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario> PegarUsuario(string usuario, string senha)
        {
            try
            {
                var dados = await _usuarioPersist.PegarUsuario(usuario, senha);
                if (dados == null)
                {
                    throw new Exception("Usuario não encontrado");
                }
                return dados;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Usuario> UpdateUsuario(string usuario, string senha, Usuario model)
        {
            try
            {
                var evento = _usuarioPersist.PegarUsuario(usuario, senha);
                if (evento == null)
                {
                    return null;
                }

                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _usuarioPersist.PegarUsuario(usuario, senha);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
