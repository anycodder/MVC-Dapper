using Dapper.Project.Core.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Dapper.Project.Data.DapperRepository;

public class UserRepository : IRepository<Users>
{
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            
            try 
            {
                _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            }
            
            catch (SqlException ex) 
            {
                throw ex; 
            }
            
        }

        public IEnumerable<Users> GetAll()
        {
            try
            {
                return _connection.Query<Users>("SELECT * FROM dbo.Users");
            }
            catch (TimeoutException ex)
            {
                
                throw ex; 
            }
            
        }

        public async Task<Users> GetById(int id)
        {
            try
            {
                return await _connection.QueryFirstOrDefaultAsync<Users>("SELECT * FROM dbo.Users WHERE id = @Id", new { Id = id });
            }
            catch (TimeoutException ex)
            {
                
                throw ex; 
            }
            
        }

        public async Task<Users> Insert(Users obj)
        {
            try
            {
                var sql = "INSERT INTO dbo.Users (user_name, user_email, user_password, user_type) VALUES (@user_name, @user_email, @user_password, @user_type)";
                await _connection.ExecuteAsync(sql, obj);
                return obj; // Return the inserted object
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw ex; // Rethrow the exception
            }
        }



        public async Task<Users> Update(Users obj)
        {
            try
            {
                var sql = "UPDATE dbo.Users SET user_name = @user_name, user_email = @user_email, user_password = @user_password, user_type = @user_type WHERE user_id = @user_id";
                await _connection.ExecuteAsync(sql, obj); // Asenkron olarak veritabanında güncelleme işlemi yapılıyor
                return obj; // Return the inserted object
            }
            catch (Exception ex)
            {
                throw ex; // Hata durumunda istisna fırlatılıyor
            }
        }

        public async Task<Users> Delete(int id)
        {
            try
            {
                var sql = "DELETE FROM dbo.Users WHERE user_id = @Id";
                await _connection.ExecuteAsync(sql, new { Id = id });
                
                var deletedUser = await _connection.QueryFirstOrDefaultAsync<Users>("SELECT * FROM dbo.Users WHERE user_id = @Id", new { Id = id });
                return deletedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

}
