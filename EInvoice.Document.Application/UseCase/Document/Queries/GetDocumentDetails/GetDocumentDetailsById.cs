using EInvoice.Document.Application.Models.Documents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.UseCase.Document.Queries.GetDocumentDetails
{
    [Authorize]
    public record GetDocumentDetailsById : IRequest<DocumentDetails>
    {
        public string uuid { get; init; }
    }
}
