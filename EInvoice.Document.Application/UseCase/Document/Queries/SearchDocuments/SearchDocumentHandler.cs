using EInvoice.Document.Application.Common;
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

namespace EInvoice.Document.Application.UseCase.Document.Queries.SearchDocuments
{
    public class SearchDocumentHandler : IRequestHandler<SearchDocument, SearchDocumentResult>
    {
        private readonly IHttpClientService _httpClientService;
        private readonly HttpClientOptions _httpClientOptions = new EInvoiceDocumentHttpClientOption();
        public SearchDocumentHandler(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        public async Task<SearchDocumentResult> Handle(SearchDocument request, CancellationToken cancellationToken)
        {
            _httpClientOptions.PathString = "/api/v1.0/documents/search";
            List<Tuple<string, string>> parameters = new List<Tuple<string, string>>()
            {
                Tuple.Create(nameof(request.uuid), request.uuid),
                Tuple.Create(nameof(request.submissionDateFrom), request.submissionDateFrom.ToString()),
                Tuple.Create(nameof(request.submissionDateTo), request.submissionDateTo.ToString()),
                Tuple.Create(nameof(request.pagesize), request.pagesize.ToString()),
                Tuple.Create(nameof(request.issueDateFrom), request.issueDateFrom.ToString()),
                Tuple.Create(nameof(request.issueDateTo), request.issueDateTo.ToString()),
                Tuple.Create(nameof(request.direction), request.direction.ToString()),
                Tuple.Create(nameof(request.status), request.status.ToString()),
                Tuple.Create(nameof(request.documentType), request.documentType.ToString()),    
                Tuple.Create(nameof(request.receiverId), request.receiverId.ToString()),
                Tuple.Create(nameof(request.receiverType), request.receiverType.ToString()),
                Tuple.Create(nameof(request.issuerTin), request.issuerTin.ToString()),
            };
            
            _httpClientOptions.QueryParameters = parameters;
            
            var httpResponseMessage = await _httpClientService.OnGet(_httpClientOptions);
            
            if (httpResponseMessage.IsSuccessStatusCode != null) //check for error later
            {
                Error error = await JsonSerializer.DeserializeAsync<Error>(httpResponseMessage.Content.ReadAsStream());
                var stringresult = await httpResponseMessage.Content.ReadAsStringAsync();
                throw new InvokeApiFailException(stringresult);
            }
            
            using var contentStream = httpResponseMessage.Content.ReadAsStream();
            
            SearchDocumentResult result = JsonSerializer.Deserialize<SearchDocumentResult>(contentStream);
            
            return result;

        }
    }
}
