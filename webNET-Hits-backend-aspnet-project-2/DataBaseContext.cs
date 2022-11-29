using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) :
            base(options)
        {
        }
    }
}
