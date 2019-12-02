using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Angular.NetCoreAPI.Common
{
    public class ApiService<T> 
    {
        private static readonly HttpClient _httpClient;

        static ApiService()
        {
            _httpClient = new HttpClient();

        }

        public async Task<List<T>> GetRecord(string _remoteServiceBaseUrl)
        {
            var responseString = await _httpClient.GetStringAsync(_remoteServiceBaseUrl);
            var catalog = JsonConvert.DeserializeObject<List<T>>(responseString);
            return catalog;
        }


        public async Task<string> PostRecordAndReturnAsString(T obj, string _remoteServiceBaseUrl)
        {
            var json = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_remoteServiceBaseUrl, stringContent);
          
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return response.IsSuccessStatusCode.ToString();
        }

        public async Task<bool> PostRecordAndReturnAsSuccess(T obj, string _remoteServiceBaseUrl)
        {
            var json = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_remoteServiceBaseUrl, stringContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

    }
 }

