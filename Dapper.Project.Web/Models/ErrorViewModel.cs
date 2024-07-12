namespace DapperProject.Web.Models

{
    public class ErrorViewModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public object detail { get; set; }
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        

        public ErrorViewModel()
        {
        
        }
        public ErrorViewModel(string status, string message)
        {
            this.status = status ;
            this.message = message;
        }
    }
}