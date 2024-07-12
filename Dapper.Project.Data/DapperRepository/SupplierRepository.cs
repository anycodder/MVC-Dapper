using Dapper.Project.Core.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dapper.Project.Data.DapperRepository
{
    public class SupplierRepository : IRepository<Suppliers>
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public SupplierRepository(IConfiguration configuration)
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

        public IEnumerable<Suppliers> GetAll()
        {
            var sql = "SELECT * FROM dbo.suppliers";

            return _connection.Query<Suppliers>(sql);
        }

        public async Task<Suppliers> GetById(int id)
        {
            var sql = @"
        SELECT *
        FROM dbo.suppliers
        WHERE id = @Id";

            var result = await _connection.QueryFirstOrDefaultAsync<Suppliers>(sql, new { Id = id });

            return result;
        }

        public async Task<Suppliers> Insert(Suppliers obj)
        {
            try
            {
                var sql = @"
                    INSERT INTO dbo.suppliers (product_id, suppliers_product_brand, suppliers_communication_information, suppliers_address, suppliers_iban) 
                    VALUES (@product_id, @suppliers_product_brand, @suppliers_communication_information, @suppliers_address, @suppliers_iban)";

                await _connection.ExecuteAsync(sql, obj);
                return obj; // Return the inserted object
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Suppliers> Update(Suppliers obj)
        {
            try
            {
                var sql = @"
                    UPDATE dbo.suppliers 
                    SET suppliers_product_brand = @suppliers_product_brand, 
                        suppliers_communication_information = @suppliers_communication_information, 
                        suppliers_address = @suppliers_address, 
                        suppliers_iban = @suppliers_iban
                    WHERE id = @id";

                await _connection.ExecuteAsync(sql, obj);
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<Suppliers> Delete(int id)
        {
            try
            {
                var sql = "DELETE FROM dbo.suppliers WHERE id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
                
                var deletedSupplier = await _connection.QueryFirstOrDefaultAsync<Suppliers>("SELECT * FROM dbo.suppliers WHERE user_id = @Id", new { Id = id });
                return deletedSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
