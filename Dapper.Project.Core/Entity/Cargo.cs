namespace Dapper.Project.Core.Entity;

public class Cargo
{
    public int id { get; set; }
    
    public int? sales_titles_id { get; set; }
    public Sales_Titles SalesTitle { get; set; }
    
    public int? user_id { get; set; }
    public Users User { get; set; }
    
    public string? cargo_brand { get; set; }
    public string? cargo_status { get; set; }
    public DateTimeOffset? cargo_estimated_delivery_date { get; set; }
    
}