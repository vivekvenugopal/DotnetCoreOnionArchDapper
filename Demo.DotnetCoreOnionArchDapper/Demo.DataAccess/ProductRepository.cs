using Demo.Definitions.DataAccess;
using Npgsql;
using Dapper;
using Demo.DTO;
using System.Data;

namespace Demo.DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;

        public ProductRepository(IDbConnection dbconnection)
        {
            _connection = dbconnection;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        { 
            return await _connection.QueryAsync<ProductDTO>("SELECT * FROM Products");
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        { 
            return await _connection.QueryFirstOrDefaultAsync<ProductDTO>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(ProductDTO product)
        { 
            var sql = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";

            await _connection.ExecuteAsync(sql, product);
        }

        public async Task UpdateAsync(ProductDTO product)
        { 
            var sql = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, product);
        }

        public async Task DeleteAsync(int id)
        { 
            var sql = "DELETE FROM Products WHERE Id = @Id";

            await _connection.ExecuteAsync(sql, new { Id = id });
        }

    }
}
