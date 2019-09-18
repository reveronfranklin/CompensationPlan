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
    public class PeriodoService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<WSMY686[]> getPeriodoAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Periodo");
            return JsonConvert.DeserializeObject<WSMY686[]>(json);
        }
        public async Task<WSMY686> GetPeriodoByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Periodo/{id}");
            return JsonConvert.DeserializeObject<WSMY686>(json);
        }

        public async Task<HttpResponseMessage> InsertPeriodoAsync(WSMY686 periodo)
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/Periodo", getStringContentFromObject(periodo));
        }

        public async Task<HttpResponseMessage> UpdatePeriodoAsync(int id, WSMY686 periodo)
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/Periodo/{id}", getStringContentFromObject(periodo));
        }

        public async Task<HttpResponseMessage> DeletePeriodoAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/Periodo/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
