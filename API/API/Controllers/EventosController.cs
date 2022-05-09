using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;
using ProEventos.Persistence;
using ProEventos.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var dados = await _eventoService.PegarEventosAsync(true);
                if (dados == null)
                {
                    return NotFound("Nenhum evento encontrado!");
                }
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar os eventos. Erro: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            try
            {
                var dados = await _eventoService.PegarEventoPorIdAsync(id, true);
                if (dados == null)
                {
                    return NotFound("Nenhum evento encontrado!");
                }
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento. Erro: {ex.Message}");
            }
        }
        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> getByTema(string tema)
        {
            try
            {
                var dados = await _eventoService.PegarEventosPorTemaAsync(tema, true);
                if (dados == null)
                {
                    return NotFound("Nenhum evento encontrado!");
                }
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar os eventos. Erro: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> IncluirEvento(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEventos(model);
                if (evento == null)
                {
                    return BadRequest("Erro ao tentar adicionar um evento");
                }
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar um evento. Erro: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterarEvento(int id, Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if (evento == null)
                {
                    return BadRequest("Erro ao tentar alterar um evento");
                }
                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar alterar um evento. Erro: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarEvento(int id)
        {
            try
            {
                if (await _eventoService.DeleteEvento(id))
                {
                    return Ok("Evento deletado");
                }
                else
                {
                    return BadRequest("Evento não deletado");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar um evento. Erro: {ex.Message}");
            }
        }
    }
}
