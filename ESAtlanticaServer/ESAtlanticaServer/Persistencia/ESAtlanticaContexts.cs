using ESAtlanticaServer.Models;
using ESAtlantica.Model;
using System.Data.Entity;

namespace ESAtlanticaServer.Persistencia
{
    public class ESAtlanticaContexts : DbContext
    {
        public ESAtlanticaContexts() : base("ESAtlantica_CS")
        {
        }

        public DbSet<Denuncia> Denuncias { get; set; }
        public DbSet<ConfiguracaoDispositivo> ConfiguracoesDispositivos { get; set; }
    }
}