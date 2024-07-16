using Dapper;
using DapperWebApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DapperWebApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Products";
                return await db.QueryAsync<Product>(sqlQuery);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "SELECT * FROM Products WHERE Id = @Id";
                return await db.QueryFirstOrDefaultAsync<Product>(sqlQuery, new { Id = id });
            }
        }

        public async Task<int> AddProductAsync(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
                return await db.ExecuteAsync(sqlQuery, product);
            }
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
                return await db.ExecuteAsync(sqlQuery, product);
            }
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string sqlQuery = "DELETE FROM Products WHERE Id = @Id";
                return await db.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
    }
}
