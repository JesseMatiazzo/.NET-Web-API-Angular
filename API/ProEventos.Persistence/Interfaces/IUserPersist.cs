using ProEventos.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface IUserPersist : IGeralPersist
    {
        Task<IEnumerable<User>> PegarUsuariosAsync();
        Task<User> PegarUsuarioPorIdAsync(int id);
        Task<User> PegarUsuarioPorNomeAsync(string usuario);
    }
}
