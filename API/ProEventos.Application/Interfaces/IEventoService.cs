using ProEventos.Application.Dtos;
using ProEventos.Persistence.Models;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface IEventoService
    {
        Task<EventosDto> AddEventos(int userId, EventosDto model);
        Task<EventosDto> UpdateEvento(int userId, int eventoId, EventosDto model);
        Task<bool> DeleteEvento(int userId, int eventoId);

        Task<PageList<EventosDto>> PegarEventosAsync(int userId, PageParams pageParams, bool includePalestrantes);
        Task<EventosDto> PegarEventoPorIdAsync(int userId, int eventoId, bool includePalestrantes);
    }
}
