using Dapper.Project.Core.Entity;
using Microsoft.Data.SqlClient;   // SqlConnection
using Microsoft.Extensions.Configuration; // IConfiguration
namespace Dapper.Project.Data.DapperRepository;

public class CommentRepository : IRepository<Comments> 
{
    private readonly IConfiguration _configuration;
    private readonly SqlConnection _connection;
    
    public CommentRepository(IConfiguration configuration)
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
    
    public IEnumerable<Comments> GetAll()
    {
        var sql = @"
            SELECT s.*, 
                   p.*,
                   u.*
        
            FROM dbo.comments s
            INNER JOIN  dbo.products p ON s.product_id = p.id
            INNER JOIN  dbo.Users u ON s.user_id = u.id";
            
        return _connection.Query<Comments,Products, Users,Comments>(sql, (comment,product , user) =>
            {
                comment.User = user;
                comment.Product = product;
                return comment;
            },
            splitOn: "id,id,id"
        );
        
    }

    public async Task<Comments> GetById(int id)
    {
        var sql = @"
        SELECT *
        FROM dbo.comments
        WHERE id = @Id";

        var result = await _connection.QueryFirstOrDefaultAsync<Comments>(sql, new { Id = id });

        return result;
    }

    
    
    public async Task<Comments> Insert(Comments obj)
    {   
        try
        {
            var sql = @"
        INSERT INTO dbo.comments (product_id, user_id, answer_id, comment_text, comment_date, comment_type, comment_score) 
        VALUES (@product_id, @user_id, @answer_id, @comment_text, @comment_date, @comment_type, @comment_score)";

            await _connection.ExecuteAsync(sql, obj);
            return obj;
        }
        catch (Exception ex)
        {
            throw ex; 
        }
    }

    
    public async Task<Comments> Update(Comments obj)
    {
        try
        {
            var sql = @"
        UPDATE dbo.comments 
        SET comment_text = @comment_text, comment_date = @comment_date, 
            comment_score = @comment_score
        WHERE id = @id";
            await _connection.ExecuteAsync(sql, obj);
            return obj;
        }
        catch (Exception ex)
        {
                
            throw ex; 
        }
    }
    
    public async Task<Comments> Delete(int id)
    {
        try
        {
            var sql = "DELETE FROM dbo.comments WHERE id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });   
            
            var deletedComments = await _connection.QueryFirstOrDefaultAsync<Comments>("SELECT * FROM dbo.comments WHERE user_id = @Id", new { Id = id });
            return deletedComments;
            
        }
        catch (Exception ex)
        {
            throw ex; 
        }
        

        
    }
}