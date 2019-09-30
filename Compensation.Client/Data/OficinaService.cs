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
    public class OficinaService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCOficina[]> GetOficinaAsync()
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Oficina");
            return JsonConvert.DeserializeObject<PCOficina[]>(json);
        }
       

    }
}
