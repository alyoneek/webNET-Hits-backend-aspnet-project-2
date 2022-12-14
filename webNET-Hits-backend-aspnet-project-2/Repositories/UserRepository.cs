using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Services;

namespace webNET_Hits_backend_aspnet_project_2.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DataBaseContext _context;
        public UserRepository(DataBaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
