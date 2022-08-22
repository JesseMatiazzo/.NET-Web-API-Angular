using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Services
{
    public class UserService : GeralServices, IUserPersist
    {
        private readonly ProEventosContext _context;
        public UserService(ProEventosContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> PegarUsuariosAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> PegarUsuarioPorIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> PegarUsuarioPorNomeAsync(string usuario)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.UserName == usuario);
        }

    }
}
