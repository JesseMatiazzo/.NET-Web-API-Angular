using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Services
{
    public class UsuarioService : IUsuarioPersist
    {
        private readonly ProEventosContext _context;

        public UsuarioService(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Usuario[]> ListarUsuarios()
        {
            return await _context.Usuarios.ToArrayAsync();
        }

        public async Task<Usuario> PegarUsuario(string usuario, string senha)
        {
            return await _context.Usuarios.Where(u => u.UsuarioLogin == usuario && u.Senha == senha).FirstOrDefaultAsync();
        }
    }
}
