using ESAtlantica.Infraestructure;
using ESAtlantica.Model;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace ESAtlantica.Dal
{
    public class DenunciaDAL
    {
        private SQLiteConnection sqlConnection;

        public DenunciaDAL()
        {
            this.sqlConnection = DependencyService.Get<IDatabaseConnection>().DbConnection();
            this.sqlConnection.CreateTable<Denuncia>();
        }

        public void Add(Denuncia denuncia)
        {
            sqlConnection.Insert(denuncia);
        }

        public void DeleteById(long id)
        {
            sqlConnection.Delete<Denuncia>(id);
        }

        public void Update(Denuncia denuncia)
        {
            sqlConnection.Update(denuncia);
        }

        public IEnumerable<Denuncia> GetAll()
        {
            return (from t in sqlConnection.Table<Denuncia>() select t).OrderBy(i => i.Denuncia_Id).ToList();
        }

        public Denuncia GetItemById(long id)
        {
            return sqlConnection.Table<Denuncia>().FirstOrDefault(t => t.Denuncia_Id == id);
        }
    }
}
