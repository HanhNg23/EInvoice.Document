using EInvoice.Document.Application.Common;
using Microsoft.Net.Http.Headers;


namespace EInvoice.Document.Application.UseCase.Document.Queries;
    public class EInvoiceDocumentHttpClientOption : HttpClientOptions
    {
        private readonly string TokenType = "Bearer";
        private readonly static string BaseUri = "https://api.myinvois.hasil.gov.my";
        private readonly Uri BaseAddressDefault = new Uri(uriString: BaseUri);
        private readonly static Dictionary<string, string> headerKeyValueDefaults = new Dictionary<string, string>()
        {
            //{HeaderNames.Accept, "application/json" },
            {HeaderNames.AcceptLanguage, "en" },
            //{HeaderNames.ContentType, "application/json" }
        };

        private readonly HttpClientOptions _httpClientOptions;
        public EInvoiceDocumentHttpClientOption()
        {
            base.BaseAddress = BaseAddressDefault;
            base.HeaderKeyValues = headerKeyValueDefaults;
            base.TokenType = TokenType;
        }
    }

