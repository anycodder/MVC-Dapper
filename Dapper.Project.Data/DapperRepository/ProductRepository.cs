using Dapper.Project.Core.Entity;
using Microsoft.Data.SqlClient;   // SqlConnection
using Microsoft.Extensions.Configuration; // IConfiguration
namespace Dapper.Project.Data.DapperRepository;

public class ProductRepository : IRepository<Products> 
{
    private readonly IConfiguration _configuration;
    private readonly SqlConnection _connection;

    public ProductRepository(IConfiguration configuration)
    {
        try
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        catch (SqlException ex) 
        {
            throw ex; 
        }
    }
    
    public IEnumerable<Products> GetAll()
    {
        try
        {
            return _connection.Query<Products>("SELECT * FROM dbo.Products");
        }
        catch (TimeoutException ex)
        {
            
            throw ex; 
        }
    }

    public async Task<Products> GetById(int id)
    {
        try
        {
            return await _connection.QueryFirstOrDefaultAsync<Products>("SELECT * FROM dbo.Products WHERE id = @Id", new { Id = id });
        }
        catch (TimeoutException ex)
        {
            
            throw ex; 
        }
    }

    public async Task<Products> Insert(Products obj)
    {
        try
        {
            var sql = "INSERT INTO dbo.Products (products_name, products_description, products_price) VALUES (@products_name, @products_description, @products_price)";
            await _connection.ExecuteAsync(sql, obj);
            return obj;
        }
        catch (Exception ex)
        {
            
            throw ex; 
        }
    }

    public async Task<Products> Update(Products obj)
    {
        try
        {
            var sql = "UPDATE dbo.Products SET products_name = @products_name, products_description = @products_description, products_price = @products_price WHERE id = @id";
            await _connection.ExecuteAsync(sql, obj);
            return obj;
        }
        catch (Exception ex)
        {
            
            throw ex; 
        }
    }
    
    public async Task<Products> Delete(int id)
    {
        try
        {
            var sql = "DELETE FROM dbo.Products WHERE id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
            
            var deletedProducts = await _connection.QueryFirstOrDefaultAsync<Products>("SELECT * FROM dbo.Products WHERE user_id = @Id", new { Id = id });
            return deletedProducts;
        }
        catch (Exception ex)
        {
            
            throw ex; 
        }
    }
}