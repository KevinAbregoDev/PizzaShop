using Microsoft.AspNetCore.Mvc;
using Models.Core;
using Service;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IPizzaService _pizzaService;
        protected IPizzaService PizzaService => 
            _pizzaService ??= HttpContext.RequestServices.GetService<IPizzaService>()
                ?? throw new InvalidOperationException("IPizza service is unavailable.");

        private IToppingService _toppingService;
        protected IToppingService ToppingService => 
            _toppingService ??= HttpContext.RequestServices.GetService<IToppingService>()
                ?? throw new InvalidOperationException("ITopping service is unavailable.");

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if (result.IsSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}