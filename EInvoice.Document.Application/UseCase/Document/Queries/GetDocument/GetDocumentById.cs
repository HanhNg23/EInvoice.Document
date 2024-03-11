using EInvoice.Document.Application.Models.Documents;
using MediatR;
using Microsoft.AspNetCore.Authorization;


namespace EInvoice.Document.Application.UseCase.Document.Queries.GetDocument
{
    [Authorize]
    public record GetDocumentById : IRequest<DocumentExtended>
    {
        public string uuid { get; init;}
        
    }
}
