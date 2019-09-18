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
    public class TasaAñoMesService
    {
        string baseUrl = Helper.BaseUrl;
        
        
        public async Task<PCTasaAñoMes[]> getTasaAñoMesnAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/TasaAñoMes");
            return JsonConvert.DeserializeObject<PCTasaAñoMes[]>(json);
        }
        public async Task<PCTasaAñoMes> GetTasaAñoMesByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/TasaAñoMes/{id}");
            return JsonConvert.DeserializeObject<PCTasaAñoMes>(json);
        }

        public async Task<HttpResponseMessage> InsertTasaAñoMesAsync(PCTasaAñoMes pCTasaAñoMes )
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/TasaAñoMes", getStringContentFromObject(pCTasaAñoMes));
        }

        public async Task<HttpResponseMessage> UpdateTasaAñoMesAsync(int id, PCTasaAñoMes pCTasaAñoMes )
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/TasaAñoMes/{id}", getStringContentFromObject(pCTasaAñoMes));
        }

        public async Task<HttpResponseMessage> DeleteTasaAñoMesAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/TasaAñoMes/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
