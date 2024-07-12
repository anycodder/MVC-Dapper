namespace Dapper.Project.Core.Entity;

public class Finance
{
    public int id { get; set; }
    
    public int? sales_titles_id { get; set; }
    public Sales_Titles Sales_Title { get; set; }
    
    public int? suppliers_id { get; set; }
    public Suppliers Supplier { get; set; }

    public DateTimeOffset? finance_date { get; set; }
    public int? finance_earnings { get; set; }
    public int? finance_commissions { get; set; }
    
}