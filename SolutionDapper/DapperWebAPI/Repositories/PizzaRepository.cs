using Dapper;
using DapperWebAPI.Context;
using DapperWebAPI.DTO;
using DapperWebAPI.Interfaces;
using DapperWebAPI.Models;
using System.Data;

namespace DapperWebAPI.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly DapperContext _context;

        public PizzaRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<Pizza> AddPizzaAsync(PizzaDTO pizza)
        {
            var query = "Insert into Pizzas values (@Name, @Price, @Toppings)" + "Select cast(scope_identity() as int)";

            var parameters = new DynamicParameters();

            parameters.Add("Name", pizza.Name, DbType.String);
            parameters.Add("Price", pizza.Price, DbType.Double);
            parameters.Add("Toppings", pizza.Toppings, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var ID = await connection.QuerySingleAsync<int>(query, parameters);

                Pizza pizza1 = new()
                {
                    Id = ID,
                    Name = pizza.Name,
                    Price = pizza.Price,
                    Toppings = pizza.Toppings
                };

                return pizza1;
            }
        }

        public async Task<Pizza> DeletePizzaAsync(int id)
        {
            var query = "DELETE FROM PIZZAS WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var pizzaToDelete = await GetPizzaAsync(id); 
                if (pizzaToDelete != null)
                {
                    await connection.ExecuteAsync(query, new { Id = id });
                }

                return pizzaToDelete;
            }
        }

        public async Task<List<Pizza>> GetAllPizzasAsync()
        {
            var query = "SELECT * FROM PIZZAS";

            using (var connection = _context.CreateConnection()) 
            {
                var pizzas = await connection.QueryAsync<Pizza>(query);
                return pizzas.ToList();
            }
        }

        public async Task<Pizza> GetPizzaAsync(int id)
        {
            var query = "SELECT * FROM PIZZAS WHERE Id = @id";

            using(var connection = _context.CreateConnection())
            {
                var pizza = await connection.QuerySingleOrDefaultAsync<Pizza>(query, new { id });
                return pizza;
            }
        }


        public async Task<Pizza> UpdatePizzaAsync(Pizza pizza)
        {
            var query = "UPDATE PIZZAS SET Name = @Name, Price = @Price, Toppings = @Toppings WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", pizza.Id, DbType.Int32);
            parameters.Add("Name", pizza.Name, DbType.String);
            parameters.Add("Price", pizza.Price, DbType.Double);
            parameters.Add("Toppings", pizza.Toppings, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return pizza;
            }
        }
    }
}
