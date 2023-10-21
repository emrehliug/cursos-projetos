using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> AddEventos(EventoDto model);
        Task<EventoDto> UpdateEvento(int eventoId, EventoDto model);
        Task<bool> DeleteEvento(int eventoId);


        Task<EventoDto[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<EventoDto[]> GetAllEventosBytemaAsync(string tema, bool includePalestrantes = false); 
        Task<EventoDto> GetAllEventoByIdAsync(int EventoId, bool includePalestrantes = false);
    }
}