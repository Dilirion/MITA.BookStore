using System;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IkitMita;

namespace BookStore.DataAccess.WebApi
{
    public abstract class WebApiClient
    {
        protected HttpClient CreateHttpClient()
        {
            var client = new HttpClient
            {
                 BaseAddress = new Uri("http://localhost:53997/api/")
            };
           
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var authToken = AuthTokenProvider?.ProvideAuthToken();
            if (!authToken.IsNullOrWhiteSpace())
            {
                client.DefaultRequestHeaders.Authorization =
                    AuthenticationHeaderValue.Parse(authToken);
            }

            return client;
        }

        protected async Task<T> GetAsync<T>(string url)
        {
            using (var client = CreateHttpClient())
            {
                var responseMessage = await client.GetAsync(url);
                responseMessage.EnsureSuccessStatusCode();
                var result = await responseMessage.Content.ReadAsAsync<T>();
                return result;
            }
        }

        public async Task<T> PostAsync<T>(string url, object obj)
        {
            using (var client = CreateHttpClient())
            {
                var responseMessage = await client.PostAsJsonAsync(url, obj);
                responseMessage.EnsureSuccessStatusCode();
                var result = await responseMessage.Content.ReadAsAsync<T>();
                return result;
            }
        }

        [Import]
        private IAuthTokenProvider AuthTokenProvider { get; set; }
    }
}
