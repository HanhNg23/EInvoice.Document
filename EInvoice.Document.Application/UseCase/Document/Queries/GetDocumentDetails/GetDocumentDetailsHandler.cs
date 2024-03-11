using EInvoice.Document.Application.Common;
using EInvoice.Document.Application.Exceptions;
using EInvoice.Document.Application.Models.Documents;
using EInvoice.Document.Application.UseCase.Document.Queries.GetDocumentDetails;
using EInvoice.Document.Infrastructure.ApiClients.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.UseCase.Document.Queries.GetDocumentDetails
{
    public class GetDocumentDetailsHandler : IRequestHandler<GetDocumentDetailsById, DocumentDetails>
    {
        private readonly IHttpClientService _httpClientService;
        private readonly HttpClientOptions _httpClientOptions = new EInvoiceDocumentHttpClientOption();
        public GetDocumentDetailsHandler(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }   
        public async Task<DocumentDetails> Handle(GetDocumentDetailsById request, CancellationToken cancellationToken)
        {
            _httpClientOptions.PathString = $"/api/v1.0/documents/{request.uuid}/raw";
            var httpResponseMessage = await _httpClientService.OnGet(_httpClientOptions);
            if (httpResponseMessage.IsSuccessStatusCode != null) //check for error later
            {
                Error error = await JsonSerializer.DeserializeAsync<Error>(httpResponseMessage.Content.ReadAsStream());
                var stringresult = await httpResponseMessage.Content.ReadAsStringAsync();
                throw new InvokeApiFailException(stringresult);
            }
            using var contentStream = httpResponseMessage.Content.ReadAsStream();
            DocumentDetails result = JsonSerializer.Deserialize<DocumentDetails>(contentStream);
            return result;
        }
    }
}
