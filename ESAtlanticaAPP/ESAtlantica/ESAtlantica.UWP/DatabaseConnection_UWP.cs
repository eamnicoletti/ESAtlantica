using ESAtlantica.Infraestructure;
using ESAtlantica.UWP;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.IO;
using Windows.Storage;


[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_UWP))]
namespace ESAtlantica.UWP
{
    public class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "ESAtlanticaBD.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            var platform = new SQLitePlatformWinRT();
            return new SQLiteConnection(platform, path);
        }
    }
}
