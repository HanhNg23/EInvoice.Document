namespace EInvoice.Document.Application.Models.Documents
{
    public class DocumentValidationResults
    {
        string status { get; set; }
        List<ValidationStepResult> validationSteps { get; set; }
    }
}