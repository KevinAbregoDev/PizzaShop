using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace API.Controllers
{
    public class PizzaController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<PizzaDTO>>> GetPizzas()
        {
            return HandleResult(await PizzaService.GetPizzas());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaDTO>> GetPizza(Guid id)
        {
            return HandleResult(await PizzaService.GetPizza(id));
        }

        [HttpPost]
        public async Task<ActionResult<PizzaDTO>> CreatePizza(PizzaDTO pizzaDTO)
        {
            return HandleResult(await PizzaService.CreatePizza(pizzaDTO));
        }

        [HttpPut]
        public async Task<ActionResult<PizzaDTO>> EditPizza(PizzaDTO pizzaDTO)
        {
            return HandleResult(await PizzaService.EditPizza(pizzaDTO));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> DeletePizza(Guid id)
        {
            return HandleResult(await PizzaService.DeletePizza(id));
        }
    }
}