using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetByUsernameAndPassword(string username, string password);
        Task<IEnumerable<User>> GetAllByRoleAsync(Role role);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUsernameAsync(string username);
    }
}
