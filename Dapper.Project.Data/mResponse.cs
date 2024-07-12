namespace DapperProject.Web.Models;

public class mResponse
{
    public string status { get; set; }
    public string message { get; set; }
    public object detail { get; set; }

    public mResponse()
    {
        
    }
    
    public mResponse(string status,string message)
    {
        this.status = status;
        this.message = message;
    }
    
    public mResponse(string status,string message,object detail)
    {
        this.status = status;
        this.message = message;
        this.detail = detail;
    }
}