using EInvoice.Document.Domain.Enumerations.DocumentEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Models.Documents
{
    public class DocumentDetails 
    {
        string uuid { get; set; }
        string submissionUid { get; set; }
        string longId { get; set; }
        string internalId { get; set; }
        string typeName { get; set; }
        string typeVersionName { get; set; }
        string issuerTin { get; set; }
        string issuerName { get; set; }
        string receiverId { get; set; }
        string receiverName { get; set; }
        DateOnly dateTimeIssued { get; set; }
        DateOnly dateTimeReceived { get; set; }
        DateOnly dateTimeValidated { get; set; }
        decimal totalSales { get; set; }
        decimal totalDiscount { get; set; }
        decimal netAmount { get; set; }
        decimal total { get; set; }
        DocumentStatus status { get; set; }
        DateOnly cancelDateTime { get; set; }
        DateOnly rejectRequestDateTime { get; set; }
        string documentStatusReason { get; set; }
        string createdByUserId { get; set; }
        DocumentValidationResults validationResults { get; set; }
    }
}
