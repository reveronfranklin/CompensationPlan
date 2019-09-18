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
    public class PorcCantidadCategoriasCubiertasService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCPorcCantidadCategoriasCubiertas[]> GetPorcCantidadCategoriasCubiertasAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/PorcCantidadCategoriasCubiertas");
            return JsonConvert.DeserializeObject<PCPorcCantidadCategoriasCubiertas[]>(json);
        }
        public async Task<PCPorcCantidadCategoriasCubiertas> GetPorcCantidadCategoriasCubiertasByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/PorcCantidadCategoriasCubiertas/{id}");
            return JsonConvert.DeserializeObject<PCPorcCantidadCategoriasCubiertas>(json);
        }

        public async Task<HttpResponseMessage> InsertPorcCantidadCategoriasCubiertasAsync(PCPorcCantidadCategoriasCubiertas pCPorcCantidadCategoriasCubiertas )
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/PorcCantidadCategoriasCubiertas", getStringContentFromObject(pCPorcCantidadCategoriasCubiertas));
        }

        public async Task<HttpResponseMessage> UpdatePorcCantidadCategoriasCubiertasAsync(int id, PCPorcCantidadCategoriasCubiertas pCPorcCantidadCategoriasCubiertas )
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/PorcCantidadCategoriasCubiertas/{id}", getStringContentFromObject(pCPorcCantidadCategoriasCubiertas));
        }

        public async Task<HttpResponseMessage> DeletePorcCantidadCategoriasCubiertasAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/PorcCantidadCategoriasCubiertas/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
