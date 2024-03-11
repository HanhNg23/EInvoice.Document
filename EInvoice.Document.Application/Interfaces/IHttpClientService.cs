using EInvoice.Document.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Infrastructure.ApiClients.Common
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> OnGet(HttpClientOptions httpClientOptions);
    }
}
