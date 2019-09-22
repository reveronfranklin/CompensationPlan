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
    public class AñoMesordenService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCAñoMesOrden[]> getAñoMesOrdenAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/AñoMesOrden");
            return JsonConvert.DeserializeObject<PCAñoMesOrden[]>(json);
        }
        
        public async Task<PCAñoMesOrden> GetAñoMesOrdenByIdAsync(string id)
        {
            Console.WriteLine("Orden desde el servicio:" + id);
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/AñoMesOrden/{id}");
            return JsonConvert.DeserializeObject<PCAñoMesOrden>(json);
        }
 
       

       

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
