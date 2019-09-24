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
    public class TipoPagoService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCTipoPago[]> GetTipoPagoAsync()
        {
            HttpClient http = new HttpClient();

            var json = await http.GetStringAsync($"{baseUrl}api/TipoPago");

            return JsonConvert.DeserializeObject<PCTipoPago[]>(json);
        }
        
        public async Task<PCTipoPago> GetTipoPagoByIdAsync(int id)
        {
            HttpClient http = new HttpClient();

            var json = await http.GetStringAsync($"{baseUrl}api/TipoPago/{id}");

            return JsonConvert.DeserializeObject<PCTipoPago>(json);
        }

        public async Task<HttpResponseMessage> InsertTipoPagoAsync(PCTipoPago TipoPago)
        {
            var client = new HttpClient();

            return await client.PostAsync($"{baseUrl}api/TipoPago", getStringContentFromObject(TipoPago));
        }

        public async Task<HttpResponseMessage> UpdateTipoPagoAsync(int id, PCTipoPago TipoPago)
        {
            var client = new HttpClient();

            return await client.PutAsync($"{baseUrl}api/TipoPago/{id}", getStringContentFromObject(TipoPago));
        }

        public async Task<HttpResponseMessage> DeleteTipoPagoAsync(int id)
        {
            var client = new HttpClient();

            return await client.DeleteAsync($"{baseUrl}api/TipoPago/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);

            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            return stringContent;
        }

    }
}
