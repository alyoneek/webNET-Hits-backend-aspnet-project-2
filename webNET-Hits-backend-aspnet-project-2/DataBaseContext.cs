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
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<DishInBasket> DishesInBasket { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .HasConversion(
                    g => g.ToString(),
                    g => (GenderType)Enum.Parse(typeof(GenderType), g));

            modelBuilder.Entity<User>()
                .HasMany(u => u.DishesInBasket)
                .WithOne()
                .IsRequired()
                .HasForeignKey(d => d.CartId);

            modelBuilder.Entity<DishCategory>()
                .HasMany(dc => dc.Dishes)
                .WithOne()
                .IsRequired()
                .HasForeignKey(d => d.DishCategoryId);

            modelBuilder.Entity<Rating>()
                .HasKey(k => new { k.UserId, k.DishId });

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion(
                    s => s.ToString(),
                    s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasDefaultValue(OrderStatus.InProcess);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderTime)
                .HasDefaultValue(DateTime.UtcNow);

        }
    }
}
