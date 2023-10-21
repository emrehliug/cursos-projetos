using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.DataContext;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Persistence
{
    public class LotePersist : ILotePersist
    {
        //Injetando o Context
        private readonly ProEventosContext DataContext;
        public LotePersist(ProEventosContext context)
        {
            DataContext = context;
            DataContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Lote[]> GetAllLotesByEventoIdAsync(int eventoId)
        {
            IQueryable<Lote> query = DataContext.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId);

            return await query.ToArrayAsync();
        }

        public async Task<Lote> GetLoteByIdAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = DataContext.Lotes;

            query = query.AsNoTracking()
                         .Where(lote => lote.EventoId == eventoId && lote.Id == id);

            return await query.FirstOrDefaultAsync();
        }
    }
}