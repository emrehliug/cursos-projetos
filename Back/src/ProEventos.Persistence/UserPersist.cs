using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.DataContext;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly ProEventosContext _context;
        public UserPersist(ProEventosContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            return await _context.Users
                            .SingleOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
        }

    }
}
