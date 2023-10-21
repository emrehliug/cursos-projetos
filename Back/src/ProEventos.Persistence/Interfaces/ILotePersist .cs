using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Interfaces
{
    public interface ILotePersist
    {
        /// <summary>
        /// Metodo que retorna todos os lotes daquele determinado Evento
        /// </summary>
        /// <param name="eventoId"></param>
        /// <returns>Lista de lotes</returns>
        Task<Lote[]> GetAllLotesByEventoIdAsync(int eventoId);

        /// <summary>
        /// Metodo que retorna 1 lote
        /// </summary>
        /// <param name="eventoId">Codigo do Evento</param>
        /// <param name="id">Codigo chave da tabela lote</param>
        /// <returns>Apenas 1 lote</returns>
        Task<Lote> GetLoteByIdAsync(int eventoId, int id);

    }
}