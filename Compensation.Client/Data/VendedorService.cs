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
    public class VendedorService
    {
        string baseUrl = Helper.BaseUrl;
        PCVendedor[] vendedorList { get; set; }
        public async Task<PCVendedor[]> GetVendedorAsync(int idOficina)
        {
           
            HttpClient http = new HttpClient();
            var json = await http.GetStringAsync($"{baseUrl}api/Vendedor");
            vendedorList = JsonConvert.DeserializeObject<PCVendedor[]>(json);
            return vendedorList.Where(v => v.CodOficina == idOficina).ToArray();
        }

       


    }
}
