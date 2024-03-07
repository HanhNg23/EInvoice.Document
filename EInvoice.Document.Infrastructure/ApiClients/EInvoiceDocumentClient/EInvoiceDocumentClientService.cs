using EInvoice.Document.Application.Common.Interfaces;
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
        private static HttpClientOptions _httpClientOptions = new EInvoiceDocumentHttpClientOptionCommon();
        private readonly IHttpClientService _httpClientService;

        public EInvoiceDocumentClientService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        public async Task<string> GetDocument(string uuid)
        {
            _httpClientOptions.PathString = "/api/v1.0/documents/{uuid}/raw";
            var httpResponseMessage = await _httpClientService.OnGet(_httpClientOptions);
            if(httpResponseMessage.Content != null) //check for error later
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                string result = await JsonSerializer.DeserializeAsync<string>(contentStream);
                return result;
            }
            return null;
        }

        public async Task<string> GetDocumentDetails(string uuid)
        {
            _httpClientOptions.PathString = "/api/v1.0/documents/{uuid}/details";
            var httpResponseMessage = await _httpClientService.OnGet(_httpClientOptions);
            if (httpResponseMessage.Content != null) //check for error later

            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                string result = await JsonSerializer.DeserializeAsync<string>(contentStream);
                return result;
            }
            return null;
        }


        public async Task<string> GetGoogleMe()
        {
            _httpClientOptions.PathString = "/oauth2/v2/userinfo"; // "/userinfo/v2/me";
            var httpResponseMessage = await _httpClientService.OnGet(_httpClientOptions);
            if (httpResponseMessage.IsSuccessStatusCode) //check for error later
            {
                //using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                //IDictionary<string, string> keyvalue = await JsonSerializer.DeserializeAsync(contentStream); //this help to convert to object
                
                
            }
            return "Empty" + httpResponseMessage.Content.ReadAsStringAsync();
        }

        public string SearchDocument(string query)
        {
            _httpClientOptions.PathString = "/api/v1.0/documents/search";
            //...
            return "Hello World";
        }

    }

}
