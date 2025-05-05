using DEPIMVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace DEPIMVC.Repository
{
    public class ApiCall<T> : IApiCall<T>
        where T : class
    {

        Uri baseaddress = new Uri("https://localhost:44312/api");
        private readonly HttpClient _client;
        public ApiCall()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseaddress;
        }


        public async Task<List<T>> GetAll(string Url)
        {
            List<T> items = new List<T>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + Url);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<T>>(data);
            }

            return items;
        }

        public async Task<T> GetById(string Url)
        {
            T item = null;
            HttpResponseMessage response = await _client.GetAsync(baseaddress + Url);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<T>(data);
            }

            return item;
        }
        public async Task<T> Insert(string url, T model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(responseContent);
                return result;
            }
            return default(T); // or `return null;` if T is a reference type
        }

        public async Task<bool> Update(string url, T model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + url, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string url, string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_client.BaseAddress}{url}/{id}");
            return response.IsSuccessStatusCode;
        }


    }
}
