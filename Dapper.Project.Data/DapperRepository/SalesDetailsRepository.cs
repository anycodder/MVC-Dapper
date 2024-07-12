using Dapper.Project.Core.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dapper.Project.Data.DapperRepository
{
    public class SalesDetailsRepository : IRepository<Sales_Details>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public SalesDetailsRepository(IConfiguration configuration)
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

        public IEnumerable<Sales_Details> GetAll()
        {
            var sql = "SELECT * FROM dbo.sales_details";

            return _connection.Query<Sales_Details>(sql);
        }

        public async Task<Sales_Details> GetById(int id)
        {
            var sql = "SELECT * FROM dbo.sales_details WHERE id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Sales_Details>(sql, new { Id = id });

            return result;
        }

        public async Task<Sales_Details> Insert(Sales_Details obj)
        {
            try
            {
                var sql = @"
                    INSERT INTO dbo.sales_details (sales_title_id, product_id, sales_detail_unit_price_of_product, sales_detail_number_of_product, sales_detail_product_payment) 
                    VALUES (@sales_title_id, @product_id, @sales_detail_unit_price_of_product, @sales_detail_number_of_product, @sales_detail_product_payment)";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sales_Details> Update(Sales_Details obj)
        {
            try
            {
                var sql = @"
                    UPDATE dbo.sales_details 
                    SET sales_title_id = @sales_title_id, 
                        product_id = @product_id, 
                        sales_detail_unit_price_of_product = @sales_detail_unit_price_of_product, 
                        sales_detail_number_of_product = @sales_detail_number_of_product, 
                        sales_detail_product_payment = @sales_detail_product_payment
                    WHERE id = @id";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Sales_Details> Delete(int id)
        {
            try
            {
                var sql = "DELETE FROM dbo.sales_details WHERE id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
                
                var deletedSalesDetails = await _connection.QueryFirstOrDefaultAsync<Sales_Details>("SELECT * FROM dbo.sales_details WHERE user_id = @Id", new { Id = id });
                return deletedSalesDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
