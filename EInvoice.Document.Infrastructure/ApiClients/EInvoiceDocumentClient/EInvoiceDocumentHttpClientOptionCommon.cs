using EInvoice.Document.Infrastructure.ApiClients.Common;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Infrastructure.ApiClients.EInvoiceDocumentClient
{
    internal class EInvoiceDocumentHttpClientOptionCommon : HttpClientOptions
    {

        private readonly string TokenType = "Bearer";
        private readonly Uri BaseAddressDefault = new Uri("https://www.googleapis.com");
        private readonly static Dictionary<string, string> headerKeyValueDefaults = new Dictionary<string, string>()
        {
            //{HeaderNames.Accept, "application/json" },
            {HeaderNames.AcceptLanguage, "en" },
            //{HeaderNames.ContentType, "application/json" }
        };

        private readonly HttpClientOptions _httpClientOptions;
        public EInvoiceDocumentHttpClientOptionCommon()
        {
            BaseAddress = BaseAddressDefault;
            HeaderKeyValues = headerKeyValueDefaults;
            base.TokenType = TokenType;
        }



    }
}
