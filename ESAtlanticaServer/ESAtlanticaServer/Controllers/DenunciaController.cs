using ESAtlanticaServer.Persistencia;
using ESAtlantica.Model;
using System.Collections.Generic;
using System.Web.Http;

namespace ESAtlanticaServer.Controllers
{
    public class DenunciaController : ApiController
    {
        private DenunciaDAL denunciaDAL = new DenunciaDAL();

        // GET: api/Denuncia
        [Route("denuncias/todas")]
        public IEnumerable<Denuncia> Get()
        {
            return denunciaDAL.GetAll();
        }

        [Route("denuncia/insert")]
        public string PostInsertDenuncia(Denuncia denuncia)
        {
            return denunciaDAL.Insert(denuncia).Numero_formulario;
        }
    }
}
