namespace Dapper.Project.Core.Entity;

public class Comments
{
    public int id { get; set; }
    
    public int? product_id { get; set; }
    public Products Product { get; set; }
   
    public int? user_id { get; set; }
    public Users User { get; set; }
    
    public int? answer_id { get; set; }
    public Comments Answer { get; set; }
    public string comment_text { get; set; } = string.Empty;
    public DateTimeOffset? comment_date { get; set; }

    public string comment_type { get; set; } = string.Empty;
    public int comment_score { get; set; }
    
}