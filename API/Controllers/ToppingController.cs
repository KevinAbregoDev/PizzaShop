using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace API.Controllers
{
    public class ToppingController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<ToppingDTO>>> GetToppings()
        {
            return HandleResult(await ToppingService.GetToppings());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToppingDTO>> GetTopping(Guid id)
        {
            return HandleResult(await ToppingService.GetTopping(id));
        }

        [HttpPost]
        public async Task<ActionResult<ToppingDTO>> CreateTopping(ToppingDTO toppingDTO)
        {
            return HandleResult(await ToppingService.CreateTopping(toppingDTO));
        }

        [HttpPut]
        public async Task<ActionResult<ToppingDTO>> EditTopping(ToppingDTO toppingDTO)
        {
            return HandleResult(await ToppingService.EditTopping(toppingDTO));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Guid>> DeleteTopping(Guid id)
        {
            return HandleResult(await ToppingService.DeleteTopping(id));
        }
    }
}