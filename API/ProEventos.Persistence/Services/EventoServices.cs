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
    public class EventoServices : IEventoPersist
    {
        private readonly ProEventosContext _context;

        public EventoServices(ProEventosContext context)
        {
            _context = context;
        }
        public async Task<Evento[]> PegarEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
            }
            query = query.AsNoTracking().OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Evento> PegarEventoPorIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
            }
            query = query.AsNoTracking().Where(e => e.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Evento[]> PegarEventosPorTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
            }
            query = query.Where(e => e.Tema.ToLower().Contains(tema.ToLower())).AsNoTracking().OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

    }
}
