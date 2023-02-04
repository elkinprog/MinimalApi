using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaStore.DTOS;

namespace PizzaStore.FluentConfig
{
    public class PizzaConfig
    {
        public PizzaConfig (EntityTypeBuilder<PizzaDTO> entity){

            entity.ToTable("Pizza");
            entity.HasKey(p => p.Id);
            entity.Property(p=> p.Name).IsRequired().HasMaxLength(50);
            entity.Property(p=>p.Description).IsRequired().HasMaxLength(50);
        }

    }
}