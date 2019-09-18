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
    public class ProductoCuotaService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCProductoCuota[]> getProductoCuota()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/ProductoCuota");
            return JsonConvert.DeserializeObject<PCProductoCuota[]>(json);
        }

        public async Task<PCProductoCuota> GetProductoCuotaByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/ProductoCuota/{id}");
            return JsonConvert.DeserializeObject<PCProductoCuota>(json);
        }

        public async Task<HttpResponseMessage> InsertProductoCuotaAsync(PCProductoCuota pCProductoCuota )
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/ProductoCuota", getStringContentFromObject(pCProductoCuota));
        }

        public async Task<HttpResponseMessage> UpdateProductoCuotaAsync(int id, PCProductoCuota pCProductoCuota )
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/ProductoCuota/{id}", getStringContentFromObject(pCProductoCuota));
        }

        public async Task<HttpResponseMessage> DeleteProductoCuotaAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/ProductoCuota/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
