namespace Dapper.Project.Core.Entity;

public class Sales_Titles
{
    public int id { get; set; }
    
    public int? user_id { get; set; }
    public Users User { get; set; }
    
    public DateTimeOffset? sales_titles_date { get; set; }
    public int sales_titles_total_price { get; set; }
    public string sales_titles_order_status { get; set; }
}