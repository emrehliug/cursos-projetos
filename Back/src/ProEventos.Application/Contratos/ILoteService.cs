using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface ILoteService
    {
        Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models);
        Task<bool> DeleteLote(int eventoId, int LoteId);

        Task<LoteDto[]> GetAllLotesByEventoIdAsync(int eventoId); 
        Task<LoteDto> GetLoteByIdAsync(int eventoId, int loteId);
    }
}