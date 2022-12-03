using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(p => p.Gender)
            .HasConversion(
                v => v.ToString(),
                v => (GenderType)Enum.Parse(typeof(GenderType), v));

            modelBuilder.Entity<DishCategory>()
                .HasMany(dc => dc.Dishes)
                .WithOne()
                .IsRequired()
                .HasForeignKey(d => d.DishCategoryId);
        }
    }
}
