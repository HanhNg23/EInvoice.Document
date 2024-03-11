using EInvoice.Document.Application.Common;
using EInvoice.Document.Application.Common.Interfaces;
using EInvoice.Document.Application.UseCase.Document.Queries;
using EInvoice.Document.Infrastructure.ApiClients.Common;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EInvoice.Document.Infrastructure.ApiClients.EInvoiceDocumentClient
{

    public class EInvoiceDocumentClientService : IEInvoiceDocumentClientService
    {
        private HttpClientOptions _httpClientOptions = new EInvoiceDocumentHttpClientOption();
        private readonly IHttpClientService _httpClientService;

        public EInvoiceDocumentClientService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<string> GetGoogleMe()
        {
            _httpClientOptions.PathString = "/oauth2/v2/userinfo"; // "/userinfo/v2/me";
            var httpResponseMessage = await _httpClientService.OnGet(_httpClientOptions);
            if (httpResponseMessage.IsSuccessStatusCode) //check for error later
            {
                //using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                //IDictionary<string, string> keyvalue = await JsonSerializer.DeserializeAsync(contentStream); //this help to convert to object
                //return await httpResponseMessage.Content.ReadAsStringAsync();
                
            }
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }

    }

}
