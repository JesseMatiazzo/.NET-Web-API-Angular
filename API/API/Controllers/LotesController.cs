using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using ProEventos.Application.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _lotesService;

        public LotesController(ILoteService loteService)
        {
            _lotesService = loteService;
        }
        [HttpGet("PegarLotesPorEvento/{eventoId}")]
        public async Task<IActionResult> PegarLotesPorEvento(int eventoId)
        {
            try
            {
                var dados = await _lotesService.PegarLotesPorEventoIdAsync(eventoId);
                if (dados == null)
                {
                    return NoContent();
                }
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar os lotes. Erro: {ex.Message}");
            }
        }
        [HttpGet("PegarTodosLotes")]
        public async Task<IActionResult> PegarTodosLotes()
        {
            try
            {
                var dados = await _lotesService.PegarTodosLotesAsync();
                if (dados == null)
                {
                    return NoContent();
                }
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar os lotes. Erro: {ex.Message}");
            }
        }
        [HttpPut("{eventoId}")]
        public async Task<IActionResult> SalvarLotes(int eventoId, LoteDto[] model)
        {
            try
            {
                var lotes = await _lotesService.SaveLote(eventoId, model);
                if (lotes == null)
                {
                    return NoContent();
                }
                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar os lotes. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> DeletarLote(int eventoId, int loteId)
        {
            try
            {
                var lote = await _lotesService.PegarLotePorIdsAsync(eventoId, loteId);
                if (lote == null)
                {
                    return NoContent();
                }
                if (await _lotesService.DeleteLote(eventoId, loteId))
                {
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Lote não deletado");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar um lote. Erro: {ex.Message}");
            }
        }
    }
}
