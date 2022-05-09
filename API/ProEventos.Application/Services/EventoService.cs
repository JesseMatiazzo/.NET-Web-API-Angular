using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.PegarEventoPorIdAsync(model.Id, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = _eventoPersist.PegarEventoPorIdAsync(eventoId, false);
                if (evento == null)
                {
                    throw new Exception("Evento para delete não foi encontrado");
                }
                _geralPersist.Delete(evento.Result);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {
                var evento = _eventoPersist.PegarEventoPorIdAsync(eventoId, false);
                if (evento == null)
                {
                    return null;
                }
                model.Id = evento.Result.Id;

                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.PegarEventoPorIdAsync(eventoId, false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Evento> PegarEventoPorIdAsync(int eventoId, bool includePalestrantes)
        {
            try
            {
                var dados = await _eventoPersist.PegarEventoPorIdAsync(eventoId, includePalestrantes);
                if (dados == null)
                {
                    return null;
                }
                return dados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> PegarEventosAsync(bool includePalestrantes)
        {
            try
            {
                var dados = await _eventoPersist.PegarEventosAsync(includePalestrantes);
                if (dados == null)
                {
                    return null;
                }
                return dados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> PegarEventosPorTemaAsync(string tema, bool includePalestrantes)
        {
            try
            {
                var dados = await _eventoPersist.PegarEventosPorTemaAsync(tema, includePalestrantes);
                if (dados == null)
                {
                    return null;
                }
                return dados;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
