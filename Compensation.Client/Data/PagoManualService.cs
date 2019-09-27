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
    public class PagoManualService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<WSMY685[]> getPagoManualAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/PagoManual");
            return JsonConvert.DeserializeObject<WSMY685[]>(json);
        }
        
        public async Task<WSMY685> GetPagoManualByIdAsync(long id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/PagoManual/{id}");
            return JsonConvert.DeserializeObject<WSMY685>(json);
        }

        public async Task<HttpResponseMessage> InsertPagoManualnAsync(WSMY685 pagoManual)
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/PagoManual", getStringContentFromObject(pagoManual));
        }

        public async Task<HttpResponseMessage> UpdatePagoManualAsync(long id, WSMY685 pagoManual)
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/PagoManual/{id}", getStringContentFromObject(pagoManual));
        }

        public async Task<HttpResponseMessage> DeletePagoManualAsync(long id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/PagoManual/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
