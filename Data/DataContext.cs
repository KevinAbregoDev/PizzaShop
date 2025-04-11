using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Topping> Toppings { get; set; }
        public DbSet<PizzaTopping> PizzaToppings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PizzaTopping>()
                .HasKey(pt => new { pt.PizzaId, pt.ToppingId });

            modelBuilder.Entity<PizzaTopping>()
                .HasOne(pt => pt.Pizza)
                .WithMany(p => p.PizzaToppings)
                .HasForeignKey(pt => pt.PizzaId);

            modelBuilder.Entity<PizzaTopping>()
                .HasOne(pt => pt.Topping)
                .WithMany(t => t.PizzaToppings)
                .HasForeignKey(pt => pt.ToppingId);
        }
    }
}