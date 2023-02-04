using Microsoft.EntityFrameworkCore;
using PizzaStore.FluentConfig;
using PizzaStore.DTOS;

namespace PizzaStore.Context
{
    public class PizzaDbContext:  DbContext
    {
    public PizzaDbContext(DbContextOptions options): base (options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        new PizzaConfig (modelBuilder.Entity<PizzaDTO>());
    }

    public DbSet<PizzaDTO> Pizza {get; set;}
    }
}