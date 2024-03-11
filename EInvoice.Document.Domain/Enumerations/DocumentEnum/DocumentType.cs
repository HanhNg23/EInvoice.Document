using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Domain.Enumerations.DocumentEnum
{
    public enum DocumentType
    {
        Invoice = 01,
        CreditNote = 02,
        DebitNote = 03,
        RefundNote = 04,
        SelfBilledInvoice = 11,
        SelfBilledCreditNote = 12,
        SelfBilledDebitNote = 13,
        SelfBilledRefundNote = 14
    }
}
