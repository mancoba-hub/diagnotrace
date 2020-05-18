using SQLite;

namespace DiagnoTrace.Services
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
