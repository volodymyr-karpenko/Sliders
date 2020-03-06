using System;
using System.Net.Http;
using Xamarin.Essentials;

namespace Sliders.Core.Services
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClient GetHttpClient()
        {
            HttpClient client = DeviceInfo.Platform == DevicePlatform.Android ? new HttpClient(GetInsecureHandler()) : new HttpClient();
            string baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:5001" : "https://localhost:5001";
            client.BaseAddress = new Uri(baseAddress);
            client.MaxResponseContentBufferSize = 5242880;
            client.Timeout = TimeSpan.FromSeconds(30);
            return client;
        }

        //AppDelegate.cs in iOS project was modified to avoid SSL errors on iOS for local secure web services

        //SSL errors can be ignored on Android for local secure web services
        private HttpClientHandler GetInsecureHandler()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost") || cert.Issuer.Equals("CN=10.0.2.2"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}