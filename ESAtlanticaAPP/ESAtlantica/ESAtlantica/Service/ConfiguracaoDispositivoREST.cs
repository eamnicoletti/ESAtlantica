using ESAtlantica.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESAtlantica.Service
{
    public class ConfiguracaoDispositivoREST
    {
        private HttpClient client;

        public ConfiguracaoDispositivoREST()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<long?> GetDispositivoIdAsync(string num_tel)
        {
            try
            {
                long? id = null;
                var uri = new Uri(string.Format("http://www.esatlantica.somee.com/dispositivos/configuracao?num_tel={0}", num_tel));
                //  var uri = new Uri(string.Format("https://esatlantica.azurewebsites.net/dispositivos/configuracao?num_tel={0}", num_tel));

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    id = JsonConvert.DeserializeObject<long>(content);
                }
                return id;

            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
