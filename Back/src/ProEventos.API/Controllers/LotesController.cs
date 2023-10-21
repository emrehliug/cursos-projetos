using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.DataContext;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService loteService;
        public LotesController(ILoteService serviceLote)
        {
            loteService = serviceLote;
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId)
        {
            try
            {
                var lotes = await loteService.GetAllLotesByEventoIdAsync(eventoId);
                if(lotes == null) return NoContent();

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError
                 , $"Erro ao tentar recuperar Lotes. Erro: {ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Put(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await loteService.SaveLotes(eventoId, models);
                if(lotes == null) return BadRequest("Erro ao tentar atualizar Lote por ID.");

                return Ok(lotes);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta atualizar Lote. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{loteId}")]
        public async Task<IActionResult> Delete(int eventoId, int loteId)
        {
            try
            {
                var lote = await loteService.GetLoteByIdAsync(eventoId, loteId);
                if (lote == null) return NoContent();

                if (await loteService.DeleteLote(lote.EventoId, lote.Id))
                    return Ok(new { message = "Deletado" });
                else
                    throw new Exception("Lote não deletado");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta deletar Lote. Erro: {ex.Message}");
            }
        }
    }
}
