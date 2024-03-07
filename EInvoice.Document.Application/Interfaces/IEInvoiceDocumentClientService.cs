using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Common.Interfaces
{
    public interface IEInvoiceDocumentClientService
    {
        public Task<string> GetDocument(string uuid);
        public Task<string> GetDocumentDetails(string uuid);
        public Task<string> GetGoogleMe();
        public string SearchDocument(string query);

    }
}
