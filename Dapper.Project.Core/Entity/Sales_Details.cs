namespace Dapper.Project.Core.Entity;

public class Sales_Details
{
    public int id { get; set; }
    
    public int? sales_title_id { get; set; }
    public Sales_Titles Sales_Title { get; set; }
    
    public int? product_id { get; set; }
    public Products Product { get; set; }

    public int? sales_detail_unit_price_of_product { get; set; }
    public int? sales_detail_number_of_product { get; set; }
    public string? sales_detail_product_payment { get; set; }
}