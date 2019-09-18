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
    public class ResumenOficinaTemporalService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCResumenOficinaTemporal[]> GetResumenOficinaTemporalAsync()
        {

            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/ResumenOficinaTemporal");
            return JsonConvert.DeserializeObject<PCResumenOficinaTemporal[]>(json);
        }
     

      

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }

    }
}
