using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface IUsuarioPersist
    {
        Task<Usuario> PegarUsuario(string usuario, string senha);
        Task<Usuario[]> ListarUsuarios();
    }
}
