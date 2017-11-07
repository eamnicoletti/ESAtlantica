using System.Web.Http;
using ESAtlanticaServer.Persistencia;

namespace ESAtlanticaServer.Controllers
{
    public class ConfiguracaoDispositivoController : ApiController
    {
        private ConfiguracaoDispositivoDAL configuracaoDispositivoDAL = new ConfiguracaoDispositivoDAL();

        [Route("dispositivos/configuracao/")]
        public long Get(string num_tel)
        {
            return (long)configuracaoDispositivoDAL.Insert(num_tel).ConfiguracaoDispositivoId;
        }
    }
}
