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
    public class TemporalService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCResumenComisionTemporal[]> getTemporalAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Temporal/GetTemporalResumen");
            return JsonConvert.DeserializeObject<PCResumenComisionTemporal[]>(json);
        }

        public async Task<PCResumenComisionTemporal[]> GetResumenTemporalByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Temporal/{id}");
            return JsonConvert.DeserializeObject<PCResumenComisionTemporal[]>(json);
        }
        public async Task<PCTemporal[]> GetDetalleTemporalByIdAsync(string id)
        {

           
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Temporal/GetTemporalVendedorDetalle/{id}");
            return JsonConvert.DeserializeObject<PCTemporal[]>(json);
        }
        public async Task<PCResumenComisionTemporal[]> GetResumenVendedoByIdAsync(int id)
        {

           
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Temporal/GetResumenVendedorDetalle/{id}");
            return JsonConvert.DeserializeObject<PCResumenComisionTemporal[]>(json);
        }


        public async Task<PCTemporal[]> GetTemporalByReciboAsync(string id)
        {


            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Temporal/GetTemporalRecibo/{id}");
            return JsonConvert.DeserializeObject<PCTemporal[]>(json);
        }

        public async Task<PCTemporal[]> GetTemporalByOrdenAsync(string id)
        {


            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Temporal/GetTemporalOrden/{id}");
            return JsonConvert.DeserializeObject<PCTemporal[]>(json);
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
