using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Core;
using Models.DTOs;

namespace Service
{
    public interface IToppingService
    {
        Task<Result<List<ToppingDTO>>> GetToppings();
        Task<Result<ToppingDTO>> GetTopping(Guid id);
        Task<Result<ToppingDTO>> CreateTopping(ToppingDTO toppingDTO);
        Task<Result<ToppingDTO>> EditTopping(ToppingDTO toppingDTO);
        Task<Result<Guid>> DeleteTopping(Guid id);
    }

    public class ToppingService : IToppingService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ToppingService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<ToppingDTO>>> GetToppings()
        {
            return Result<List<ToppingDTO>>.Success(
                await _context.Toppings
                .ProjectTo<ToppingDTO>(_mapper.ConfigurationProvider)
                .ToListAsync());
        }

        public async Task<Result<ToppingDTO>> GetTopping(Guid id)
        {
            return Result<ToppingDTO>.Success(
                await _context.Toppings
                .ProjectTo<ToppingDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == id)
            );
        }

        public async Task<Result<ToppingDTO>> CreateTopping(ToppingDTO toppingDTO)
        {
            var newTopping = _mapper.Map<Topping>(toppingDTO);

            _context.Toppings.Add(newTopping);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) 
                return Result<ToppingDTO>.Failure("Failed to create the topping.");

            return Result<ToppingDTO>.Success(toppingDTO);
        }

        public async Task<Result<ToppingDTO>> EditTopping(ToppingDTO toppingDTO)
        {
            var updatedTopping = await _context.Toppings.FindAsync(toppingDTO.Id);

            if (updatedTopping == null) 
                return Result<ToppingDTO>.Failure("Topping does not exist.");

            _mapper.Map(toppingDTO, updatedTopping);

            var result = await _context.SaveChangesAsync() > 0;
            
            if (!result) 
                return Result<ToppingDTO>.Failure("Failed to update the topping.");

            return Result<ToppingDTO>.Success(toppingDTO);
        }

        public async Task<Result<Guid>> DeleteTopping(Guid id)
        {
            var topping = await _context.Toppings.FindAsync(id);

            if (topping == null) 
                return Result<Guid>.Failure("Topping was not found.");

            _context.Remove(topping);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) 
                return Result<Guid>.Failure("Failed to delete the topping.");

            return Result<Guid>.Success(topping.Id);
        }
    }
}