using DapperWebAPI.DTO;
using DapperWebAPI.Interfaces;
using DapperWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebAPI.Controllers
{
    [Route("api/pizzas")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaRepository _pizza;
        public PizzaController(IPizzaRepository pizza)
        {
            _pizza = pizza;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> GetPizzas()
        {
            var pizzas = await _pizza.GetAllPizzasAsync();
            return Ok(pizzas);
        }


        [HttpGet("{id}", Name = "Pizza By Id")]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            var pizza = await _pizza.GetPizzaAsync(id);
            if (pizza is null)
                return NotFound(id);
            return Ok(pizza);
        }


        [HttpPost]
        public async Task<ActionResult<Pizza>> AddPizza(PizzaDTO pizza)
        {
            var result = await _pizza.AddPizzaAsync(pizza);
            return CreatedAtRoute("Pizza By Id", new { id = result.Id },result);
        }

        [HttpDelete]
        public async Task<ActionResult<Pizza>> DeletePizza(int id)
        {
            var pizza = await _pizza.DeletePizzaAsync(id);
            return Ok(pizza);
        }

        [HttpPut]
        public async Task<ActionResult<Pizza>> UpdatePizza(Pizza pizza)
        {
            var updatedPizza = await _pizza.UpdatePizzaAsync(pizza);
            return Ok(updatedPizza);
        }
            
    }

}
