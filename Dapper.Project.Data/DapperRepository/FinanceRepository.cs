using Dapper.Project.Core.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dapper.Project.Data.DapperRepository
{
    public class FinanceRepository : IRepository<Finance>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public FinanceRepository(IConfiguration configuration)
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

        public IEnumerable<Finance> GetAll()
        {
            var sql = "SELECT * FROM dbo.finance";

            return _connection.Query<Finance>(sql);
        }

        public async Task<Finance> GetById(int id)
        {
            var sql = "SELECT * FROM dbo.finance WHERE id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Finance>(sql, new { Id = id });

            return result;
        }

        public async Task<Finance> Insert(Finance obj)
        {
            try
            {
                var sql = @"
                    INSERT INTO dbo.finance (sales_titles_id, suppliers_id, finance_date, finance_earnings, finance_commissions) 
                    VALUES (@sales_titles_id, @suppliers_id, @finance_date, @finance_earnings, @finance_commissions)";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Finance> Update(Finance obj)
        {
            try
            {
                var sql = @"
                    UPDATE dbo.finance 
                    SET sales_titles_id = @sales_titles_id, 
                        suppliers_id = @suppliers_id, 
                        finance_date = @finance_date, 
                        finance_earnings = @finance_earnings, 
                        finance_commissions = @finance_commissions
                    WHERE id = @id";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Finance> Delete(int id)
        {
            try
            {
                var sql = "DELETE FROM dbo.finance WHERE id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
                
                var deletedFinance = await _connection.QueryFirstOrDefaultAsync<Finance>("SELECT * FROM dbo.finance WHERE user_id = @Id", new { Id = id });
                return deletedFinance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
