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
    public class CalculoService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCProceso[]> EjecutarCalculoAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Calculo");
            return JsonConvert.DeserializeObject<PCProceso[]>(json);
        }

        public async Task<HttpResponseMessage> UpdatePCProcesoAsync(int id, PCProceso pCProceso)
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/Calculo/{id}", getStringContentFromObject(pCProceso));
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
