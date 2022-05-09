using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventoPersist
    {
        Task<Evento[]> PegarEventosPorTemaAsync(string tema, bool includePalestrantes);
        Task<Evento[]> PegarEventosAsync(bool includePalestrantes);
        Task<Evento> PegarEventoPorIdAsync(int eventoId, bool includePalestrantes);

    }
}
