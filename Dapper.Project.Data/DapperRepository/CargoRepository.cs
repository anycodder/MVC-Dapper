using Dapper.Project.Core.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dapper.Project.Data.DapperRepository
{
    public class CargoRepository : IRepository<Cargo>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public CargoRepository(IConfiguration configuration)
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

        public IEnumerable<Cargo> GetAll()
        {
            var sql = @"
                SELECT c.*,
                       st.*,
                       u.*
                FROM dbo.cargo c
                INNER JOIN dbo.sales_titles st ON c.sales_titles_id = st.id
                INNER JOIN dbo.Users u ON c.user_id = u.id";

            return _connection.Query<Cargo, Sales_Titles, Users, Cargo>(sql, (cargo, salesTitle, user) =>
            {
                cargo.SalesTitle = salesTitle;
                cargo.User = user;
                return cargo;
            },
            splitOn: "id,id,id");
        }

        public async Task<Cargo> GetById(int id)
        {
            var sql = @"
        SELECT *
        FROM dbo.cargo
        WHERE id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Cargo>(sql, new { Id = id });

            return result;
        }

        public async Task<Cargo> Insert(Cargo obj)
        {
            try
            {
                var sql = @"
                    INSERT INTO dbo.cargo (sales_titles_id, user_id, cargo_brand, cargo_status, cargo_estimated_delivery_date)
                    VALUES (@sales_titles_id, @user_id, @cargo_brand, @cargo_status, @cargo_estimated_delivery_date)";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cargo> Update(Cargo obj)
        {
            try
            {
                var sql = @"
                    UPDATE dbo.cargo 
                    SET cargo_brand = @cargo_brand, 
                        cargo_status = @cargo_status, 
                        cargo_estimated_delivery_date = @cargo_estimated_delivery_date
                    WHERE id = @id";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cargo> Delete(int id)
        {
            try
            {
                var sql = "DELETE FROM dbo.cargo WHERE id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
                
                var deletedCargo = await _connection.QueryFirstOrDefaultAsync<Cargo>("SELECT * FROM dbo.cargo WHERE user_id = @Id", new { Id = id });
                return deletedCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
