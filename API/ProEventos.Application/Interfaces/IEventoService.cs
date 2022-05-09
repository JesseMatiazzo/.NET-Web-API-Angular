using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<Evento> AddEventos(Evento model);
        Task<Evento> UpdateEvento(int eventoId, Evento model);
        Task<bool> DeleteEvento(int eventoId);

        Task<Evento[]> PegarEventosPorTemaAsync(string tema, bool includePalestrantes);
        Task<Evento[]> PegarEventosAsync(bool includePalestrantes);
        Task<Evento> PegarEventoPorIdAsync(int eventoId, bool includePalestrantes);
    }
}
