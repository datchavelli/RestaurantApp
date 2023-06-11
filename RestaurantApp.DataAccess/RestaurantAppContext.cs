using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain.Entities;
using System;

namespace RestaurantApp.DataAccess
{
    public class RestaurantAppContext : DbContext
    {
        public RestaurantAppContext()
        {
        }

        public RestaurantAppContext(DbContextOptions otp) : base(otp)
        { 
            Database.EnsureCreated();
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=RestaurantApp;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantAppContext).Assembly);
            modelBuilder.Entity<RoleUseCase>().HasKey(x => new { x.RoleId, x.UseCaseId });
            modelBuilder.Entity<Role>().Property(x => x.IsDefault).HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<RoleUseCase> RoleUseCases { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

    }
}
