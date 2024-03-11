using EInvoice.Document.Application.Models.Documents;
using EInvoice.Document.Domain.Enumerations.DocumentEnum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.UseCase.Document.Queries.SearchDocuments
{
    [Authorize]
    public record SearchDocument : IRequest<SearchDocumentResult>
    {
        public string? uuid { get; init; }
        public DateTime? submissionDateFrom { get; init; }
        public DateTime? submissionDateTo { get; init; }
        public string? pagesize { get; init; }
        public DateTime? issueDateFrom { get; init; }
        public DateTime? issueDateTo { get; init; }
        public Direction? direction { get; init; } //direction enum - Possible values: sent, received
        public DocumentStatus? status { get; init; } //documentstatus enum - Possible values: valid, invalid, canceled, rejected
        public DocumentType? documentType { get; init; } //documenttype enum - Possible values: 01,02.///
        public string? receiverId { get; init; } //Only can be used when ‘Direction’ filter is set to Sent ; Possible values: (Business registration number, Passport Number, National ID)
        public ReceiverType? receiverType { get; init; } //Only can be used when ‘Direction’ filter is set to Sent ; Possible values: (BRN, PASSPORT, NRIC, ARMY)
        public Direction? issuerTin { get; init; } //Only can be used when ‘Direction’ filter is set to Received




    }
}
