using EInvoice.Document.Domain.Enumerations.DocumentEnum;

namespace EInvoice.Document.Application.Models.Documents
{
    public class ValidationStepResult
    {
        string name { get; set; }
        DocumentValidationStatus status { get; set; }
        Error error { get; set; }
    }
}