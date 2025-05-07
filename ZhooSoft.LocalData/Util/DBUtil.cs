using SQLite;

namespace ZhooSoft.LocalData.Util
{
    public class DBUtil : IDBUtil
    {
        #region Fields

        public SQLiteConnection database;

        #endregion

        #region Constructors

        public DBUtil()
        {
        }

        public DBUtil(string dbName, bool IsDeleteDB, string password)
        {
            Init(dbName, 1, password, IsDeleteDB);
        }

        #endregion

        #region Methods

        public async Task CreateDataBaseAsync(string dbName, bool IsDeleteDB, string password)
        {
            try
            {
                if (File.Exists(DBConstants.DatabasePath) && IsDeleteDB)
                {
                    Directory.Delete(DBConstants.DatabasePath);
                }

                database = new SQLiteConnection(DBConstants.DatabasePath, DBConstants.Flags);
            }
            catch (Exception ex)
            {
                var err = ex.Message;
            }
        }

        public SQLiteConnection GetDataBase()
        {
            return database;
        }

        public async void Init(string dbName, int dbVersion, string password, bool IsDeleteDB)
        {
            await CreateDataBaseAsync(dbName, IsDeleteDB, password);
        }

        #endregion
    }
}
