using ProEventos.Domain;
using ProEventos.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface IEventoPersist
    {
        Task<PageList<Evento>> PegarEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false);
        Task<Evento> PegarEventoPorIdAsync(int userId, int eventoId, bool includePalestrantes);

    }
}
