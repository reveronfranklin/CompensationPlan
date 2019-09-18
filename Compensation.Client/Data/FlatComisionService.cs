using Compensaction.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Compensation.Client.Data
{
    public class FlatComisionService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCFlatComision[]> getFlatComisionAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/FlatComision");
            return JsonConvert.DeserializeObject<PCFlatComision[]>(json);
        }
        
        public async Task<PCFlatComision> GetFlatComisionByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/FlatComision/{id}");
            return JsonConvert.DeserializeObject<PCFlatComision>(json);
        }

        public async Task<HttpResponseMessage> InsertFlatComisionAsync(PCFlatComision flatComision)
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/FlatComision", getStringContentFromObject(flatComision));
        }

        public async Task<HttpResponseMessage> UpdateFlatComisionAsync(int id, PCFlatComision flatComision)
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/FlatComision/{id}", getStringContentFromObject(flatComision));
        }

        public async Task<HttpResponseMessage> DeleteFlatComisionAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/FlatComision/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
