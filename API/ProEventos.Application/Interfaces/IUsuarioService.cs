using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> AddUsuario(Usuario model);
        Task<Usuario> UpdateUsuario(string usuario, string senha, Usuario model);
        Task<bool> DeleteUsuario(string usuario, string senha);

        Task<Usuario> PegarUsuario(string usuario, string senha);
        Task<Usuario[]> ListarUsuarios();
    }
}
