using ESAtlanticaServer.Models;
using System.Linq;

namespace ESAtlanticaServer.Persistencia
{
    public class ConfiguracaoDispositivoDAL
    {
        private ESAtlanticaContexts contexto = new ESAtlanticaContexts();

        public ConfiguracaoDispositivo Insert(string num_tel)
        {
            ConfiguracaoDispositivo cd = GetConfiguracaoDispositivo(num_tel);
            if (cd == null)
            {
                cd = contexto.ConfiguracoesDispositivos.Add(
                    new ConfiguracaoDispositivo() { Num_tel = num_tel }
                    );
                contexto.SaveChanges();
            }
            return cd;
        }

        private ConfiguracaoDispositivo GetConfiguracaoDispositivo(string num_tel)
        {
            return contexto.ConfiguracoesDispositivos.Where(e => e.Num_tel == num_tel).FirstOrDefault();
        }
    }
}