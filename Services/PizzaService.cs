using Models;
using Models.Core;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Service
{
    public interface IPizzaService
    {
        public Task<Result<List<PizzaDTO>>> GetPizzas();
        public Task<Result<PizzaDTO>> GetPizza(Guid id);
        public Task<Result<PizzaDTO>> CreatePizza(PizzaDTO pizzaDTO);
        public Task<Result<PizzaDTO>> EditPizza(PizzaDTO pizzaDTO);
        public Task<Result<Guid>> DeletePizza(Guid id);
    }

    public class PizzaService : IPizzaService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PizzaService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<PizzaDTO>>> GetPizzas()
        {
            return Result<List<PizzaDTO>>.Success(
                await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .ProjectTo<PizzaDTO>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }

        public async Task<Result<PizzaDTO>> GetPizza(Guid id)
        {
            return Result<PizzaDTO>.Success(
                await _context.Pizzas
                .Include(p => p.PizzaToppings)
                .ThenInclude(pt => pt.Topping)
                .ProjectTo<PizzaDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id)
            );
        }

        public async Task<Result<PizzaDTO>> CreatePizza(PizzaDTO pizzaDTO)
        {
            var newPizza = _mapper.Map<Pizza>(pizzaDTO);

            _context.Pizzas.Add(newPizza);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) 
                return Result<PizzaDTO>.Failure("Failed to create the pizza.");

            return Result<PizzaDTO>.Success(pizzaDTO);
        }

        public async Task<Result<PizzaDTO>> EditPizza(PizzaDTO pizzaDTO)
        {
            var updatedPizza = await _context.Pizzas.FindAsync(pizzaDTO.Id);

            if (updatedPizza == null) 
                return Result<PizzaDTO>.Failure("Pizza does not exist.");

            _mapper.Map(pizzaDTO, updatedPizza);

            var result = await _context.SaveChangesAsync() > 0;
            
            if (!result) 
                return Result<PizzaDTO>.Failure("Failed to update the pizza.");

            return Result<PizzaDTO>.Success(pizzaDTO);
        }

        public async Task<Result<Guid>> DeletePizza(Guid id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza == null) 
                return Result<Guid>.Failure("Pizza was not found.");

            _context.Remove(pizza);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) 
                return Result<Guid>.Failure("Failed to delete the pizza.");

            return Result<Guid>.Success(pizza.Id);
        }
    }
}