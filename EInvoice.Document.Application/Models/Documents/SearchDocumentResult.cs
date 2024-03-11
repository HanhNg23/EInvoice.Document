using EInvoice.Document.Domain.Enumerations.DocumentEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Models.Documents
{
    public class SearchDocumentResult
    {
        string uuid { get; set; }   
        string submissionUUID { get; set; } 
        string  longId { get; set; }
        string internalId { get; set; }
        string typeName { get; set; }
        string typeVersionName { get; set; }
        string issuerTin { get; set; }
        string issuerName { get; set; }
        string receiverId { get; set; }
        string receiverIdType { get; set; }
        string receiverName { get; set; }
        DateTime dateTimeIssued { get; set; }
        DateTime dateTimeReceived { get; set; }
        /// <summary>
        /// Total sales amount of the document in MYR
        /// </summary>
        decimal totalSalesMYR { get; set; }
        /// <summary>
        /// Total discount amount of the document in MYR
        /// </summary>
        decimal totalDiscountMYR { get; set; }
        /// <summary>
        /// total net amount of the document in MYR
        /// </summary>
        decimal netAmountMYR { get; set; }
        /// <summary>
        /// Total amount of the document in MYR
        /// </summary>
        decimal totalMYR { get; set; }
        /// <summary>
        /// Total sales amount of the document in Original currency
        /// </summary>
        decimal totalSales { get; set; }
        /// <summary>
        /// Total discount amount of the document in Original currency.
        /// </summary>
        decimal totalDiscount { get; set; }
        /// <summary>
        /// Total net amount of the document in Original currency.
        /// </summary>
        decimal netAmount { get; set; }
        /// <summary>
        /// Total amount of the document in Original currency
        /// </summary>
        decimal total { get; set; }
        DocumentStatus status { get; set; }
        DateOnly cancelDateTime { get; set; }
        DateOnly rejectRequestDateTime { get; set; }
        string documentStatusReason { get; set; }
        string createdByUserId { get; set; }

    }
}
