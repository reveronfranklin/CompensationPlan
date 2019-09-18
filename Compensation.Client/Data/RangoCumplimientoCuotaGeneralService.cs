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
    public class RangoCumplimientoCuotaGeneralService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCRangoCumplimientoCuotaGeneral[]> getRangoCumplimientoCuotaGeneralServiceAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/RangoCumplimientoCuotaGeneral");
            return JsonConvert.DeserializeObject<PCRangoCumplimientoCuotaGeneral[]>(json);
        }

        public async Task<PCRangoCumplimientoCuotaGeneral> GetRangoCumplimientoCuotaGeneralByIdAsync(int id)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/RangoCumplimientoCuotaGeneral/{id}");
            return JsonConvert.DeserializeObject<PCRangoCumplimientoCuotaGeneral>(json);
        }

        public async Task<HttpResponseMessage> InsertRangoCumplimientoCuotaGeneralAsync(PCRangoCumplimientoCuotaGeneral pCRangoCumplimientoCuotaGeneral )
        {
            var client = new HttpClient();
            return await client.PostAsync($"{baseUrl}api/RangoCumplimientoCuotaGeneral", getStringContentFromObject(pCRangoCumplimientoCuotaGeneral));
        }

        public async Task<HttpResponseMessage> UpdateRangoCumplimientoCuotaGeneralAsync(int id, PCRangoCumplimientoCuotaGeneral pCRangoCumplimientoCuotaGeneral )
        {
            var client = new HttpClient();
            return await client.PutAsync($"{baseUrl}api/RangoCumplimientoCuotaGeneral/{id}", getStringContentFromObject(pCRangoCumplimientoCuotaGeneral));
        }

        public async Task<HttpResponseMessage> DeleteRangoCumplimientoCuotaGeneralAsync(int id)
        {
            var client = new HttpClient();
            return await client.DeleteAsync($"{baseUrl}api/RangoCumplimientoCuotaGeneral/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
