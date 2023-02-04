using AutoMapper;
using PizzaStore.DTOS;
using PizzaStore.Models;

namespace PizzaStore.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile(){
             CreateMap<Pizza, PizzaDTO>().ReverseMap();
        }
    }
}

