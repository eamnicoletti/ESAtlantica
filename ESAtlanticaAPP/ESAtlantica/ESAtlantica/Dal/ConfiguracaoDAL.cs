using System.Linq;
using Xamarin.Forms;
using ESAtlantica.Infraestructure;
using ESAtlantica.Model;
using SQLite.Net;

namespace ESAtlantica.Dal
{
    public class ConfiguracaoDAL
    {
        private SQLiteConnection sqlConnection;

        public ConfiguracaoDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<ConfiguracaoDispositivo>();
        }

        public ConfiguracaoDispositivo GetConfiguracao()
        {
            return (from t in sqlConnection.Table<ConfiguracaoDispositivo>() select t).FirstOrDefault();
        }

        public void Add(ConfiguracaoDispositivo configuracaoDispositivo)
        {
            sqlConnection.Insert(configuracaoDispositivo);
        }
    }
}
