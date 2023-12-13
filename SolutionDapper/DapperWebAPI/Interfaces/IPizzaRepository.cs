using DapperWebAPI.DTO;
using DapperWebAPI.Models;

namespace DapperWebAPI.Interfaces
{
    public interface IPizzaRepository
    {
        Task <Pizza> GetPizzaAsync (int id);  
        Task<List<Pizza>> GetAllPizzasAsync ();
        Task<Pizza> AddPizzaAsync(PizzaDTO pizza);  
        Task<Pizza> DeletePizzaAsync (int id);
        Task<Pizza> UpdatePizzaAsync(Pizza pizza);

    }
}
