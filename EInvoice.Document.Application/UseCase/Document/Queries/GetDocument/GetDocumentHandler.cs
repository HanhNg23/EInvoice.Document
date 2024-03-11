using EInvoice.Document.Application.Common;
using EInvoice.Document.Application.Common.Interfaces;
using EInvoice.Document.Application.Exceptions;
using EInvoice.Document.Application.Models.Documents;
using EInvoice.Document.Infrastructure.ApiClients.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.UseCase.Document.Queries.GetDocument
{
    public class GetDocumentHandler : IRequestHandler<GetDocumentById, DocumentExtended>
    {
        private readonly IHttpClientService _httpClientService;
        private readonly HttpClientOptions _httpClientOptions = new EInvoiceDocumentHttpClientOption();
        public GetDocumentHandler(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        public async Task<DocumentExtended> Handle(GetDocumentById request, CancellationToken cancellationToken)
        {
            _httpClientOptions.PathString = $"/api/v1.0/documents/{request.uuid}/raw";
            var httpResponseMessage = await _httpClientService.OnGet(_httpClientOptions);
            if (!httpResponseMessage.IsSuccessStatusCode) //check for error later
            {
                Error error = await JsonSerializer.DeserializeAsync<Error>(httpResponseMessage.Content.ReadAsStream());
                var stringresult = await httpResponseMessage.Content.ReadAsStringAsync();
                throw new InvokeApiFailException(stringresult);
            }
            using var contentStream = httpResponseMessage.Content.ReadAsStream();
            DocumentExtended result = JsonSerializer.Deserialize<DocumentExtended>(contentStream);
            return result;
        }
    }
}
