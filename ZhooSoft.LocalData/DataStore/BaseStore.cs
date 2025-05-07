using SQLite;
using System.Linq.Expressions;
using ZhooSoft.LocalData.Util;

namespace ZhooSoft.LocalData.DataStore
{
    public class BaseStore<T> : IBaseStore<T> where T : class, new()
    {
        #region Fields

        private IDBUtil _dBUtil;

        #endregion

        #region Constructors

        public BaseStore(IDBUtil dBUtil)
        {
            _dBUtil = dBUtil;
        }

        #endregion

        #region Methods

        public virtual int Delete(T item)
        {
            try
            {
                var database = GetConnectionAsync();
                return database.Delete(item);
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public virtual int DeleteAll()
        {
            var database = GetConnectionAsync();
            return database.DeleteAll<T>();
        }

        public int DeleteAll(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var database = GetConnectionAsync();
                var items = database.Table<T>().Where(predicate).ToList();
                foreach (var item in items)
                {
                    Delete(item);
                }
            }
            catch (Exception ex)
            {
                return 1;
            }
            return 1;
        }

        public virtual SQLiteConnection GetConnectionAsync()
        {
            return _dBUtil.GetDataBase();
        }

        public virtual int GetCount()
        {
            var db = GetConnectionAsync();
            return db.Table<T>().Count();
        }

        public T GetItem(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var database = GetConnectionAsync();
                var item = database.Table<T>().FirstOrDefault(predicate);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public virtual T GetItem(string id)
        {
            var db = GetConnectionAsync();
            return db.Find<T>(id);
        }

        public virtual T GetItemIntPk(int id)
        {
            var db = GetConnectionAsync();
            return db.Find<T>(id);
        }

        public virtual IEnumerable<T> GetItems()
        {
            var db = GetConnectionAsync();
            try
            {
                return db.Table<T>().ToList();
            }
            catch (Exception)
            {
                return new List<T>();
            }
        }

        public List<T> GetItems(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var database = GetConnectionAsync();
                var item = database.Table<T>().Where(predicate).ToList();
                return item;
            }
            catch (Exception ex)
            {
                return new List<T>();
            }
        }

        public virtual int Insert(T item)
        {
            var database = GetConnectionAsync();
            return database.Insert(item);
        }

        public virtual int InsertAll(IEnumerable<T> items)
        {
            var database = GetConnectionAsync();
            return database.InsertAll(items);
        }

        public virtual bool IsPresent(string id)
        {
            var db = GetConnectionAsync();
            var item = db.Find<T>(id);
            return item != null;
        }

        public virtual int Update(T item)
        {
            var database = GetConnectionAsync();
            return database.Update(item);
        }

        public virtual int UpdateAll(IEnumerable<T> items)
        {
            var database = GetConnectionAsync();
            return database.UpdateAll(items);
        }

        #endregion
    }
}
