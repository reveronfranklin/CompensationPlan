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
    public class SysFileService
    {
        string baseUrl = Helper.BaseUrl;

        public async Task<PCSysFile[]> GetSysFileAsync()
        {
            HttpClient http = new HttpClient();

            var json = await http.GetStringAsync($"{baseUrl}api/SysFile");

            return JsonConvert.DeserializeObject<PCSysFile[]>(json);
        }
        
        public async Task<PCSysFile> GetSysFileByIdAsync(int id)
        {
            HttpClient http = new HttpClient();

            var json = await http.GetStringAsync($"{baseUrl}api/SysFile/{id}");

            return JsonConvert.DeserializeObject<PCSysFile>(json);
        }

        public async Task<HttpResponseMessage> InsertSysFileAsync(PCSysFile sysFile)
        {
            var client = new HttpClient();

            return await client.PostAsync($"{baseUrl}api/SysFile", getStringContentFromObject(sysFile));
        }

        public async Task<HttpResponseMessage> UpdateSysFileAsync(int id, PCSysFile sysFile)
        {
            var client = new HttpClient();

            return await client.PutAsync($"{baseUrl}api/SysFile/{id}", getStringContentFromObject(sysFile));
        }

        public async Task<HttpResponseMessage> DeleteSysFileAsync(int id)
        {
            var client = new HttpClient();

            return await client.DeleteAsync($"{baseUrl}api/SysFile/{id}");
        }

        private StringContent getStringContentFromObject(object obj)
        {
            var serialized = JsonConvert.SerializeObject(obj);

            var stringContent = new StringContent(serialized, Encoding.UTF8, "application/json");

            return stringContent;
        }

    }
}
