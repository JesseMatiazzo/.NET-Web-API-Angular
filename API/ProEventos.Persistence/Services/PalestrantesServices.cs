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
    public class PalestrantesPersistence : IPalestrantePersist
    {
        private readonly ProEventosContext _context;

        public PalestrantesPersistence(ProEventosContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<Evento[]> PegarEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
            }
            query = query.OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Evento> PegarEventoPorIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
            }
            query = query.Where(e => e.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<Evento[]> PegarEventosPorTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos.Include(e => e.Lotes).Include(e => e.RedesSociais);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestrantesEventos).ThenInclude(e => e.Palestrante);
            }
            query = query.Where(e => e.Tema.ToLower().Contains(tema.ToLower())).OrderBy(e => e.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> PegarPalestrantePorIdAsync(int eventoId, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
            }
            query = query.Where(p => p.Id == eventoId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> PegarPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
            }
            query = query.OrderBy(p => p.User.PrimeiroNome);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> PegarPalestrantesPorNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.Include(p => p.RedesSociais);
            if (includeEventos)
            {
                query = query.Include(p => p.PalestrantesEventos).ThenInclude(p => p.Evento);
            }
            query = query.Where(p => p.User.PrimeiroNome.ToLower().Contains(nome.ToLower())).OrderBy(p => p.User.PrimeiroNome);
            return await query.ToArrayAsync();
        }

    }
}
