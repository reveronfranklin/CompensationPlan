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
    public class OrdenesPignoradasService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCOrdenesPignoradas[]> getOrdenesPignoradasAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/OrdenesPignoradas");
            return JsonConvert.DeserializeObject<PCOrdenesPignoradas[]>(json);
        }
        
        public async Task<PCOrdenesPignoradas> GetOrdenesPignoradasByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/OrdenesPignoradas/{id}");
            return JsonConvert.DeserializeObject<PCOrdenesPignoradas>(json);
        }

        public async Task<HttpResponseMessage> InsertOrdenesPignoradasAsync(PCOrdenesPignoradas pCOrdenesPignoradas )
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/OrdenesPignoradas", getStringContentFromObject(pCOrdenesPignoradas));
        }

        public async Task<HttpResponseMessage> UpdateOrdenesPignoradasAsync(int id, PCOrdenesPignoradas pCOrdenesPignoradas)
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/OrdenesPignoradas/{id}", getStringContentFromObject(pCOrdenesPignoradas));
        }

        public async Task<HttpResponseMessage> DeleteOrdenesPignoradasAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/OrdenesPignoradas/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
