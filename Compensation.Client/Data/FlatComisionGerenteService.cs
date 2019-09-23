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
    public class FlatComisionGerenteService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCFlatComisionGerente[]> getFlatComisionAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/FlatComisionGerente");
            return JsonConvert.DeserializeObject<PCFlatComisionGerente[]>(json);
        }
        
        public async Task<PCFlatComisionGerente> GetFlatComisionByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/FlatComisionGerente/{id}");
            return JsonConvert.DeserializeObject<PCFlatComisionGerente>(json);
        }

        public async Task<HttpResponseMessage> InsertFlatComisionAsync(PCFlatComisionGerente flatComision)
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/FlatComisionGerente", getStringContentFromObject(flatComision));
        }

        public async Task<HttpResponseMessage> UpdateFlatComisionAsync(int id, PCFlatComisionGerente flatComision)
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/FlatComisionGerente/{id}", getStringContentFromObject(flatComision));
        }

        public async Task<HttpResponseMessage> DeleteFlatComisionAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/FlatComisionGerente/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
