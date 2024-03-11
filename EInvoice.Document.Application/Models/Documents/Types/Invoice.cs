using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Models.Documents.Types
{
    public class Invoice
    {
        Supplier AccountingSupplierParty { get; set; }
        Buyer AccountingCustomerParty { get; set; }
        string eInvoiceVersion { get; set; }
        string InvoiceTypeCode { get; set; }
        string listVersionID { get; set; } //attribute of InvoiceTypeCode
        string ID { get; set; } //e-invoice Code/Number
        DateOnly IssueDate { get; set; }
        TimeOnly IssueTime { get; set; }
    }
}
