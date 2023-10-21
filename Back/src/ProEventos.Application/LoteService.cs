using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application
{
    public class LoteService : ILoteService
    {
        private readonly IGeralPersist geralPersist;
        private readonly ILotePersist lotePersist;
        private readonly IMapper mapper;
        public LoteService(IGeralPersist geral, ILotePersist lote, IMapper mapper)
        {
            geralPersist = geral;
            lotePersist = lote;
            this.mapper = mapper;
        }
        public async Task AddLote(int eventoId, LoteDto model)
        {
            try
            {
                var lote = mapper.Map<Lote>(model);
                lote.EventoId = eventoId;

                geralPersist.Add<Lote>(lote);

                await geralPersist.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models)
        {
            try
            {
                var lotes = await lotePersist.GetAllLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        await AddLote(eventoId, model);
                    }
                    else
                    {
                        var lote = lotes.FirstOrDefault(x => x.Id == model.Id);
                        var loteGravar = mapper.Map(model, lote);

                        model.EventoId = eventoId;

                        geralPersist.Update<Lote>(loteGravar);

                        await geralPersist.SaveChangesAsync();
                    }
                }

                var retorno = await lotePersist.GetAllLotesByEventoIdAsync(eventoId);
                return mapper.Map<LoteDto[]>(retorno);
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
                var lote = await lotePersist.GetLoteByIdAsync(eventoId, loteId);
                if (lote == null) throw new Exception("Evento para delete não encontrado.");

                geralPersist.Delete(lote);
                return await geralPersist.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoteDto[]> GetAllLotesByEventoIdAsync(int eventoId)
        {
            try
            {
                var lotes = await lotePersist.GetAllLotesByEventoIdAsync(eventoId);
                if (lotes == null) return null;

                var resultado = mapper.Map<LoteDto[]>(lotes);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LoteDto> GetLoteByIdAsync(int EventoId, int LoteId)
        {
            try
            {
                var lote = await lotePersist.GetLoteByIdAsync(EventoId, LoteId);
                if (lote == null) return null;

                var resultado = mapper.Map<LoteDto>(lote);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}