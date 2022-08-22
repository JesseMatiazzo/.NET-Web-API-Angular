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
    public class LoteServices : ILotePersist
    {
        private readonly ProEventosContext _context;

        public LoteServices(ProEventosContext context)
        {
            _context = context;
        }

        public async Task<Lote> PegarLotePorIdAsync(int eventoId, int idLote)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking().Where(lote => lote.EventoId == eventoId
            && lote.Id == idLote);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Lote[]> PegarLotesPorEventoAsync(int eventoId)
        {
            IQueryable<Lote> query = _context.Lotes;

            query = query.AsNoTracking().Where(lote => lote.EventoId == eventoId);
            return await query.ToArrayAsync();
        }

        public async Task<Lote[]> PegarTodosLotesAsync()
        {
            IQueryable<Lote> query = _context.Lotes;

            return await query.ToArrayAsync();
        }
    }
}
