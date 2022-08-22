using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Services
{
    public class EventoServices : IEventoPersist
    {
        private readonly ProEventosContext _context;

        public EventoServices(ProEventosContext context)
        {
            _context = context;
        }
        public async Task<PageList<Evento>> PegarEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
            }
            query = query.AsNoTracking().Where(e => e.Tema.ToLower().Contains(pageParams.Terms.ToLower()) && e.UserId == userId).AsNoTracking().OrderBy(e => e.Id);
            return await PageList<Evento>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }
        public async Task<Evento> PegarEventoPorIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
            }
            query = query.AsNoTracking().Where(e => e.Id == eventoId && e.UserId == userId);
            return await query.FirstOrDefaultAsync();
        }

    }
}
