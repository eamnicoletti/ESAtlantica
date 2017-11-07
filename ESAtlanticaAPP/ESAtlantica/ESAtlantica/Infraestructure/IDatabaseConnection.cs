using SQLite.Net;

namespace ESAtlantica.Infraestructure
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
}
