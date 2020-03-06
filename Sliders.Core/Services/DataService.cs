using MvvmCross;
using MvvmCross.Base;
using Sliders.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Sliders.Core.Services
{
    public class DataService : IDataService<SlidersData>
    {
        private readonly HttpClient _client;
        private readonly IMvxJsonConverter _serializer;

        public DataService(IHttpClientService httpClientService)
        {
            _client = httpClientService.GetHttpClient();
            _serializer = Mvx.IoCProvider.Resolve<IMvxJsonConverter>();
        }

        private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<IEnumerable<SlidersData>> ReadAllDataAsync()
        {
            if (IsConnected)
            {
                var json = await _client.GetStringAsync($"api/SlidersData");
                return await Task.Run(() => _serializer.DeserializeObject<IEnumerable<SlidersData>>(json));
            }

            return null;
        }

        public async Task<SlidersData> ReadDataAsync(string id)
        {
            if (!string.IsNullOrEmpty(id) && IsConnected)
            {
                var json = await _client.GetStringAsync($"api/SlidersData/{id}");
                return await Task.Run(() => _serializer.DeserializeObject<SlidersData>(json));
            }

            return null;
        }

        public async Task<bool> UpdateDataAsync(SlidersData item)
        {
            if (item == null || string.IsNullOrEmpty(item.Id) || !IsConnected)
            {
                return false;
            }                

            var serializedItem = _serializer.SerializeObject(item);

            var response = await _client.PutAsync(new Uri($"api/SlidersData/{item.Id}"), new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateDataAsync(SlidersData item)
        {
            if (item == null || !IsConnected)
            {
                return false;
            }                

            var serializedItem = _serializer.SerializeObject(item);

            var response = await _client.PostAsync($"api/SlidersData", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDataAsync(string id)
        {
            if (string.IsNullOrEmpty(id) || !IsConnected)
            {
                return false;
            }                

            var response = await _client.DeleteAsync($"api/SlidersData/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAllDataAsync()
        {
            if (!IsConnected)
            {
                return false;
            }                

            var response = await _client.DeleteAsync($"api/SlidersData");

            return response.IsSuccessStatusCode;
        }
    }
}