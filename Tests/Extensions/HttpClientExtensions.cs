using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tests.Extensions
{
    internal static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> SendJsonPost<TCommand>(this HttpClient client, string requestUri, TCommand command)
        {
            var json = JsonConvert.SerializeObject(command);
            var httpContent = new StringContent(json, Encoding.Default, "application/json");

            return client.PostAsync(requestUri, httpContent);
        }
    }
}