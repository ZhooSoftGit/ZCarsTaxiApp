namespace ZhooSoft.LocalData
{
    public static class DBConstants
    {
        #region Constants

        public const string DatabaseFilename = "ZCarsSQLite.db3";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        public const string DB_VERSION_LABEL = "dbversion";

        public const string DB_VERSION = "1.0";

        #endregion

        #region Properties

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        #endregion
    }
}
