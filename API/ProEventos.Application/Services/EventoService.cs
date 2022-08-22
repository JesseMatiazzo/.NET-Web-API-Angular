using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.Models;
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
        private readonly IMapper _mapper;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
            _mapper = mapper;
        }
        public async Task<EventosDto> AddEventos(int userId, EventosDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                evento.UserId = userId;

                _geralPersist.Add(evento);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoResult = await _eventoPersist.PegarEventoPorIdAsync(userId, evento.Id, false);
                    return _mapper.Map<EventosDto>(eventoResult);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            try
            {
                var evento = _eventoPersist.PegarEventoPorIdAsync(userId, eventoId, false);
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
        public async Task<EventosDto> UpdateEvento(int userId, int eventoId, EventosDto model)
        {
            try
            {
                var evento = await _eventoPersist.PegarEventoPorIdAsync(userId, eventoId, false);
                if (evento == null)
                {
                    return null;
                }
                model.Id = evento.Id;
                model.UserId = userId;
                _mapper.Map(model, evento);
                _geralPersist.Update(evento);

                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoResult = await _eventoPersist.PegarEventoPorIdAsync(userId, evento.Id, false);
                    return _mapper.Map<EventosDto>(eventoResult);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventosDto> PegarEventoPorIdAsync(int userId, int eventoId, bool includePalestrantes)
        {
            try
            {
                var dados = await _eventoPersist.PegarEventoPorIdAsync(userId, eventoId, includePalestrantes);
                if (dados == null)
                {
                    return null;
                }
                var resultado = _mapper.Map<EventosDto>(dados);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<EventosDto>> PegarEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
        {
            try
            {
                var dados = await _eventoPersist.PegarEventosAsync(userId, pageParams, includePalestrantes);
                if (dados == null)
                {
                    return null;
                }
                var resultado = _mapper.Map<PageList<EventosDto>>(dados);

                resultado.CurrentPage = dados.CurrentPage;
                resultado.TotalPages = dados.TotalPages;
                resultado.PageSize = dados.PageSize;
                resultado.TotalCount = dados.TotalCount;

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
