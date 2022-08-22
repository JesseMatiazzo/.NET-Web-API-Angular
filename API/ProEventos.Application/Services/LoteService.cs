using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application.Services
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly ILotePersist _lotePersist;
        private readonly IMapper _mapper;

        public LoteService(IGeralPersist geralPersist, ILotePersist lotePersist, IMapper mapper)
        {
            _geralPersist = geralPersist;
            _lotePersist = lotePersist;
            _mapper = mapper;
        }

        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;
                _geralPersist.Add(lote);
                await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = _mapper.Map<Lote>(model);
                lote.EventoId = eventoId;
                _geralPersist.Update(lote);
                await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> SaveLote(int eventoid, LoteDto[] models)
        {
            try
            {
                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoid, model);
                    }
                    else
                    {
                        await UpdateLote(eventoid, model);
                    }
                }
                var lotesRetorno = _lotePersist.PegarLotesPorEventoAsync(eventoid).Result;
                return _mapper.Map<LoteDto[]>(lotesRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteLote(int eventoId, int loteId)
        {
            try
            {
                var lote = _lotePersist.PegarLotePorIdAsync(eventoId, loteId);
                if (lote == null)
                {
                    throw new Exception("Lote para delete não foi encontrado");
                }
                _geralPersist.Delete(lote.Result);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoteDto> PegarLotePorIdsAsync(int eventoId, int loteId)
        {
            try
            {
                var dados = await _lotePersist.PegarLotePorIdAsync(eventoId, loteId);
                if (dados == null)
                {
                    return null;
                }
                var resultado = _mapper.Map<LoteDto>(dados);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> PegarLotesPorEventoIdAsync(int eventoId)
        {
            try
            {
                var dados = await _lotePersist.PegarLotesPorEventoAsync(eventoId);
                if (dados == null)
                {
                    return null;
                }
                var resultado = _mapper.Map<LoteDto[]>(dados);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto[]> PegarTodosLotesAsync()
        {
            try
            {
                var dados = await _lotePersist.PegarTodosLotesAsync();
                if (dados == null)
                {
                    return null;
                }
                return _mapper.Map<LoteDto[]>(dados);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
