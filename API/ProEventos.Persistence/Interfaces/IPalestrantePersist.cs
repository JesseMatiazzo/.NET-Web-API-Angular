using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface IPalestrantePersist
    {
        Task<Palestrante[]> PegarPalestrantesPorNomeAsync(string nome, bool includeEvento);
        Task<Palestrante[]> PegarPalestrantesAsync(bool includeEvento);
        Task<Palestrante> PegarPalestrantePorIdAsync(int eventoId, bool includeEvento);

    }
}
