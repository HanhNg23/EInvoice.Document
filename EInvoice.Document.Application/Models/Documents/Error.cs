namespace EInvoice.Document.Application.Models.Documents
{
    public class Error
    {
        string code { get; set; }
        string message { get; set; }
        string target { get; set; }
        List<Error> details { get; set; }
    }
}