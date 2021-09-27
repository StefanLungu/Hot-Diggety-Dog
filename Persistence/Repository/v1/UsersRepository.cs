using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Persistence.Repository.v1
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(DataContext context) : base(context)
        {

        }

        public async Task<User> GetByUsernameAndPassword(string username, string password)
        {
            return await _context.Users
                .Where(user => user.Username == username && user.Password == Crypto.SHA256(password))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllByRoleAsync(Role role)
        {
            return await _context.Users.Where(user => user.Role.Equals(role)).ToListAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(user => user.Username == username);
        }
    }
}
