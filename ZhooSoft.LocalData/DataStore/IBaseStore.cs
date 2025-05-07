using SQLite;
using System.Linq.Expressions;

namespace ZhooSoft.LocalData.DataStore
{
    #region Interfaces

    public interface IBaseStore<T> where T : class, new()
    {
        #region Methods

        int Delete(T item);

        int DeleteAll();

        int DeleteAll(Expression<Func<T, bool>> predicate);

        SQLiteConnection GetConnectionAsync();

        int GetCount();

        T GetItem(Expression<Func<T, bool>> predicate);

        T GetItem(string id);

        IEnumerable<T> GetItems();

        List<T> GetItems(Expression<Func<T, bool>> predicate);

        int Insert(T item);

        int InsertAll(IEnumerable<T> items);

        bool IsPresent(string id);

        int Update(T item);

        int UpdateAll(IEnumerable<T> items);

        #endregion
    }

    #endregion
}
