using EInvoice.Document.Application.Common;
using EInvoice.Document.Application.Interfaces;
using System.Net.Http.Headers;
using System.Text;


namespace EInvoice.Document.Infrastructure.ApiClients.Common
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private IUser _user;
        public HttpClientService(HttpClient httpClient, IUser user)
        {
            _httpClient = httpClient;
            _user = user;
        }

        public async Task<HttpResponseMessage> OnGet(HttpClientOptions httpClientOptions)
        {
            Console.WriteLine("PathString: " + httpClientOptions.PathString);
            await SettingHttpClient(httpClientOptions);
            Console.WriteLine("Headers TOI NGOAI ============== ");
            foreach (var item in _httpClient.DefaultRequestHeaders.ToDictionary())
            {
                Console.WriteLine("- Key: " + item.Key);
                foreach (var value in item.Value)
                {
                    Console.WriteLine("  + Value: " + value);
                }
            }
            Console.WriteLine("Authorization TOI NGOAI" + _httpClient.DefaultRequestHeaders.Authorization);
            var responseMessage = await _httpClient.GetAsync(httpClientOptions.PathString);
            return responseMessage;
        }

        private async Task SettingHttpClient(HttpClientOptions httpClientOptions)
        {
            //base address
            _httpClient.BaseAddress = httpClientOptions.BaseAddress;
            if (string.IsNullOrEmpty(httpClientOptions.PathString))
            {
                httpClientOptions.PathString = "";
            }
            //headers
            //_httpClient.DefaultRequestHeaders.Clear();

            httpClientOptions.AccessToken = await _user.AccessToken;
            Console.WriteLine("AccessToken TÔI ĐÂY: " + httpClientOptions.AccessToken);
            if (!string.IsNullOrEmpty(httpClientOptions.AccessToken) && !string.IsNullOrEmpty(httpClientOptions.TokenType))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(httpClientOptions.TokenType, httpClientOptions.AccessToken);
                Console.WriteLine("Authorization TOI DAY: " + _httpClient.DefaultRequestHeaders.Authorization.ToString());

            }
            if (httpClientOptions.HeaderKeyValues != null && httpClientOptions.HeaderKeyValues.Count > 0)
            {
                foreach (var item in httpClientOptions.HeaderKeyValues)
                {
                    _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    // _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, )
                }
            }
            //query parameters

            if (httpClientOptions.QueryParameters != null && httpClientOptions.QueryParameters.Count > 0)
            {
                var lookup = httpClientOptions.QueryParameters.ToLookup(x => x.Item1, x => x.Item2);
                var query = new StringBuilder();
                foreach (var item in lookup)
                {
                    query.Append($"{item.Key}={item}" + "&");

                }
                var queryStr = query.ToString().TrimEnd('&');
                httpClientOptions.PathString += "?" + queryStr;
            }

        }

    }
}
