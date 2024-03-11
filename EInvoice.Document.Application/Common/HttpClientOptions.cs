using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EInvoice.Document.Application.Common
{
    public class HttpClientOptions
    {
        public Uri? BaseAddress { get; set; }
        public string? PathString { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? AccessToken { get; set; }
        public string? TokenType { get; set; }
        public IDictionary<string, string>? HeaderKeyValues { get; set; }
        public List<Tuple<string,string>>? QueryParameters { get; set; }

    }

}
