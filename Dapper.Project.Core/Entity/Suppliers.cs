namespace Dapper.Project.Core.Entity;

public class Suppliers
{
    public int id { get; set; }
    
    public int? product_id { get; set; }
    public Products Product { get; set; }
    
    public string suppliers_product_brand { get; set; }
    public string? suppliers_communication_information { get; set; }
    public string? suppliers_address { get; set; }
    public string suppliers_iban { get; set; } 
}