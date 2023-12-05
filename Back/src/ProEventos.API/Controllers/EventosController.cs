using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
    public class EventosController : ControllerBase
    {      
        private readonly IEventoService eventoService;
        public IWebHostEnvironment hostEnvironment { get; }

        public EventosController(IEventoService serviceEvento, IWebHostEnvironment HostEnvironment)
        {
            eventoService = serviceEvento;
            hostEnvironment = HostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError
                 , $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await eventoService.GetAllEventoByIdAsync(id, true);
                if (evento == null) return NoContent();

                return Ok(evento);
            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError
                 , $"Erro ao tenta recupera o evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await eventoService.GetAllEventosBytemaAsync(tema, true);
                if(eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                 return this.StatusCode(StatusCodes.Status500InternalServerError
                 , $"Erro ao tenta recupera eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImage(int eventoId)
        {
            try
            {
                var evento = await eventoService.GetAllEventoByIdAsync(eventoId);
                if (evento == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeletaImagem(evento.ImagemURL);
                    evento.ImagemURL = await SaveImagem(file);
                }

                var eventoRetorno = await eventoService.UpdateEvento(evento.Id, evento);

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = await eventoService.AddEventos(model);
                if(evento == null) return BadRequest("Erro ao tentar adicionar evento.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
            try
            {
                var evento = await eventoService.UpdateEvento(id, model);
                if(evento == null) return BadRequest("Erro ao tentar atualizar evento por ID.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta atualizar evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await eventoService.GetAllEventoByIdAsync(id, true);
                if (evento == null) return NoContent();

                if (await eventoService.DeleteEvento(id))
                {
                    DeletaImagem(evento.ImagemURL);
                    return Ok(new { message = "Deletado" });
                }
                else
                {
                    throw new Exception("Evento não deletado");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError
                , $"Erro ao tenta deletar evento. Erro: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<String> SaveImagem(IFormFile image)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(image.FileName).Take(10).ToArray()).Replace(' ','-');
            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(image.FileName)}";
            
            var imagePath = Path.Combine(hostEnvironment.ContentRootPath, @"Resources/images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        public void DeletaImagem(string img)
        {
            var imagePath = Path.Combine(hostEnvironment.ContentRootPath, @"Resources/images", img);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            
        }
    }
}
