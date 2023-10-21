using System;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist geralPersist;
        private readonly IEventoPersist eventoPersist;
        private readonly IMapper mapper;
        public EventoService(IGeralPersist geral, IEventoPersist evento, IMapper mapper)
        {
            geralPersist = geral;
            eventoPersist = evento;
            this.mapper = mapper;
        }
        public async Task<EventoDto> AddEventos(EventoDto model)
        {
            try
            {
                var evento = mapper.Map<Evento>(model);

                geralPersist.Add<Evento>(evento);
                if (await geralPersist.SaveChangesAsync())
                {
                    var retorno = await eventoPersist.GetEventoByIdAsync(evento.Id, false);
                    return mapper.Map<EventoDto>(retorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto> UpdateEvento(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) return null;

                model.Id = eventoId;

                var eventoGravar = mapper.Map<Evento>(model);
                
                geralPersist.Update<Evento>(eventoGravar);
                if (await geralPersist.SaveChangesAsync())
                {
                    var retorno = await eventoPersist.GetEventoByIdAsync(eventoGravar.Id, false);  
                    return mapper.Map<EventoDto>(retorno);
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
                var evento = await eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento == null) throw new Exception("Evento para delete não encontrado.");

                geralPersist.Delete(evento);
                return await geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;

                var resultado = mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetAllEventoByIdAsync(int EventoId, bool includePalestrantes = false)
        {
            try
            {
                var evento = await eventoPersist.GetEventoByIdAsync(EventoId, includePalestrantes);
                if (evento == null) return null;

                var resultado = mapper.Map<EventoDto>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosBytemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await eventoPersist.GetAllEventosBytemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;

                var resultado = mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}