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
    public class CuotaVentasService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCCuotaVentas[]> getCuotaVentasAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/CuotaVentas");
            return JsonConvert.DeserializeObject<PCCuotaVentas[]>(json);
        }

        public async Task<PCCuotaVentas> GetCuotaVentasByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/CuotaVentas/{id}");
            return JsonConvert.DeserializeObject<PCCuotaVentas>(json);
        }

        public async Task<HttpResponseMessage> InsertCuotaVentasAsync(PCCuotaVentas pCCuotaVentas )
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/CuotaVentas", getStringContentFromObject(pCCuotaVentas));
        }

        public async Task<HttpResponseMessage> UpdateFlatComisionAsync(int id, PCCuotaVentas pCCuotaVentas )
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/CuotaVentas/{id}", getStringContentFromObject(pCCuotaVentas));
        }

        public async Task<HttpResponseMessage> DeleteCuotaVentasAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/CuotaVentas/{id}");
        }

        public async Task<PCCuotaVentas[]> GetCuotaVentasByVendedorAsync(string id)
        {


            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/CuotaVentas/GetCuotaVendedor/{id}");
            return JsonConvert.DeserializeObject<PCCuotaVentas[]>(json);
        }


        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
