using AutoMapper;
using Models.DTOs;

namespace Models.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pizza, PizzaDTO>()
                .ForMember(dto => dto.Id, o => o.MapFrom(p => p.Id))
                .ForMember(dto => dto.Name, o => o.MapFrom(p => p.Name))
                .ForMember(dto => dto.Price, o => o.MapFrom(p => p.Price))
                .ForMember(dto => dto.Toppings, o => o.MapFrom(
                    p => p.PizzaToppings.Select(pt => 
                    new ToppingDTO(){ Id = pt.Topping.Id, Name = pt.Topping.Name, Price = pt.Topping.Price})
                    .ToList()));

            CreateMap<PizzaDTO, Pizza>()
                .ForMember(p => p.Id, o => o.MapFrom(dto => dto.Id))
                .ForMember(p => p.Name, o => o.MapFrom(dto => dto.Name))
                .ForMember(p => p.Price, o => o.MapFrom(dto => dto.Price))
                .ForMember(p => p.PizzaToppings, o => o.MapFrom(
                    dto => dto.Toppings.Select(t =>
                    new PizzaTopping(){ ToppingId = t.Id,
                    Topping = new Topping(){ Id = t.Id, Name = t.Name, Price = t.Price}})
                    .ToList()));

            CreateMap<Topping, ToppingDTO>()
                .ForMember(dto => dto.Id, o => o.MapFrom(t => t.Id))
                .ForMember(dto => dto.Name, o => o.MapFrom(t => t.Name))
                .ForMember(dto => dto.Price, o => o.MapFrom(t => t.Price));

            CreateMap<ToppingDTO, Topping>()
                .ForMember(t => t.Id, o => o.MapFrom(dto => dto.Id))
                .ForMember(t => t.Name, o => o.MapFrom(dto => dto.Name))
                .ForMember(t => t.Price, o => o.MapFrom(dto => dto.Price));
        }
    }
}