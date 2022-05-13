using SEC.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SEC.Utilities
{
    public class HttpWebClient: IHttpWebClient
    {
        public NetworkCredential Credential { get; set; }
        public WebProxy Proxy { get; set; }
        public TimeSpan Timeout { get; set; }

        public HttpWebClient()
        {
            Timeout = TimeSpan.FromSeconds(120);
        }

        public async Task<string> SendGetAsync(string url)
        {
            var response = await GetAsync(string.Format(url), null, false);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                //TODO for search result unsuccessful expection handle
                //ThrowExcption(response, url);
            }

            //return _jsonSerializer.Deserialize<TResponse>(content);
            return content;
        }

        private async Task<HttpResponseMessage> GetAsync(string requestUri, Dictionary<string, string> requestHeaders = null,
                bool raiseExceptionIfHttpStatusNotOk = true)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            using (var httpClientHandler = new HttpClientHandler() { Proxy = Proxy })
            using (var client = new HttpClient(httpClientHandler) { Timeout = Timeout })
            {               

                if (requestHeaders != null)
                    foreach (var header in requestHeaders)
                    {
                        request.Headers.Add(header.Key, header.Value);
                    }

                var response = await client.SendAsync(request).ConfigureAwait(false);
                if (raiseExceptionIfHttpStatusNotOk) response = response.EnsureSuccessStatusCode();
                return response;
            }
        }
    }
}
