using SQLite;

namespace ZhooSoft.LocalData.Util
{
    #region Interfaces

    public interface IDBUtil
    {
        #region Methods

        Task CreateDataBaseAsync(string dbName, bool IsDeleteDB, string password);

        SQLiteConnection GetDataBase();

        void Init(string dbName, int dbVersion, string password, bool IsDeleteDB);

        #endregion
    }

    #endregion
}
