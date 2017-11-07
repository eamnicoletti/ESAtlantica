using ESAtlantica.Dal;
using ESAtlantica.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ESAtlantica.Service
{
    public class DenunciaREST
    {
        private HttpClient client;

        public DenunciaREST()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<Denuncia> AddDenunciaAsync(Denuncia denuncia)
        {
            try
            {
                string numeroFormulario;
                var uri = new Uri(string.Format("http://www.esatlantica.somee.com/denuncia/insert"));
                var denunciaDAL = new DenunciaDAL();

                var json = JsonConvert.SerializeObject(denuncia);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    numeroFormulario = await response.Content.ReadAsStringAsync();
                    denuncia.Numero_formulario = numeroFormulario;
                    denuncia.Situacao = true;
                    return denuncia;
                }
                return denuncia;
            }
            catch (Exception ex)
            {
                throw ex;
            }                
        }

        public async Task<List<Denuncia>> GetDenunciasAsync()
        {
            try
            {
                var uri = new Uri(string.Format("http://www.esatlantica.somee.com/denuncias/todas"));

                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Denuncia>>(content);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
