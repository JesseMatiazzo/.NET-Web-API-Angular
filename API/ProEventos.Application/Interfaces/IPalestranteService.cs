using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    interface IPalestranteService
    {
        Task<Palestrante> AddEventos(Palestrante model);
        Task<Palestrante> UpdateEvento(int palestranteId, Palestrante model);
        Task<bool> DeleteEvento(int palestranteId);

        Task<Palestrante[]> PegarPalestrantesPorNomeAsync(string nome, bool includeEvento);
        Task<Palestrante[]> PegarPalestrantesAsync(bool includeEvento);
        Task<Palestrante> PegarPalestrantePorIdAsync(int eventoId, bool includeEvento);

    }
}
