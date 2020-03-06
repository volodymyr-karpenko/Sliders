using System.Net.Http;

namespace Sliders.Core.Services
{
    public interface IHttpClientService
    {
        HttpClient GetHttpClient();
    }
}