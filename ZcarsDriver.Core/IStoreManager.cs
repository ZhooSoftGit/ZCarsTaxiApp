using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCarsDriver.Core
{
    public interface IStoreManager
    {
        Task<bool> Init(string key);
        void ClearDB();
        void ClearAppDB();
        void DropTable(SQLiteConnection db);
    }
}
