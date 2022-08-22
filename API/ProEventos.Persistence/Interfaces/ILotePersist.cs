using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface ILotePersist
    {
        Task<Lote[]> PegarLotesPorEventoAsync(int eventoId);
        Task<Lote> PegarLotePorIdAsync(int eventoId, int idLote);
        Task<Lote[]> PegarTodosLotesAsync();

    }
}
