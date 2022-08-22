using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface ILoteService
    {
        Task<LoteDto[]> SaveLote(int eventoid, LoteDto[] models);
        Task<bool> DeleteLote(int eventoId, int loteId);

        Task<LoteDto[]> PegarLotesPorEventoIdAsync(int eventoId);
        Task<LoteDto[]> PegarTodosLotesAsync();
        Task<LoteDto> PegarLotePorIdsAsync(int eventoId, int loteId);
    }
}
