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
    public class HistoricoService
    {
        string baseUrl = Helper.BaseUrl;



        public async Task<PCHistorico[]> GetPCHistoricoByIdPeriodoAsync(int id,string vendedor)
        {
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Historico/{id}/{vendedor}");
            return JsonConvert.DeserializeObject<PCHistorico[]>(json);
        }

       
      

        




        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);
            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");
            return stringContent;
        }


        public async Task<PCHistorico[]> GetHistoricoByIOrdenAsync(string id)
        {


            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Historico/GetHistoricoOrden/{id}");
            return JsonConvert.DeserializeObject<PCHistorico[]>(json);
        }

        public async Task<PCHistorico[]> GetHistoricoByReciboAsync(string id)
        {


            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Historico/GetHistoricoRecibo/{id}");
            return JsonConvert.DeserializeObject<PCHistorico[]>(json);
        }
    }
}
