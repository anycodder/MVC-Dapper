using Dapper.Project.Core.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dapper.Project.Data.DapperRepository
{
    public class SalesTitlesRepository : IRepository<Sales_Titles>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public SalesTitlesRepository(IConfiguration configuration)
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

        public IEnumerable<Sales_Titles> GetAll()
        {
            var sql = "SELECT * FROM dbo.sales_titles";

            return _connection.Query<Sales_Titles>(sql);
        }

        public async Task<Sales_Titles> GetById(int id)
        {
            var sql = "SELECT * FROM dbo.sales_titles WHERE id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Sales_Titles>(sql, new { Id = id });

            return result;
        }

        public async Task<Sales_Titles> Insert(Sales_Titles obj)
        {
            try
            {
                var sql = @"
                    INSERT INTO dbo.sales_titles (user_id, sales_titles_date, sales_titles_total_price, sales_titles_order_status) 
                    VALUES (@user_id, @sales_titles_date, @sales_titles_total_price, @sales_titles_order_status)";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sales_Titles> Update(Sales_Titles obj)
        {
            try
            {
                var sql = @"
                    UPDATE dbo.sales_titles 
                    SET user_id = @user_id, 
                        sales_titles_date = @sales_titles_date, 
                        sales_titles_total_price = @sales_titles_total_price, 
                        sales_titles_order_status = @sales_titles_order_status
                    WHERE id = @id";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sales_Titles> Delete(int id)
        {
            try
            {
                var sql = "DELETE FROM dbo.sales_titles WHERE id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
                
                var deletedSalesTitles = await _connection.QueryFirstOrDefaultAsync<Sales_Titles>("SELECT * FROM dbo.sales_titles WHERE user_id = @Id", new { Id = id });
                return deletedSalesTitles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
