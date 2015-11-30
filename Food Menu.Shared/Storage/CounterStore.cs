using Food_Menu.Storage.Models;
using Food_Menu.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Storage
{
    public class CounterStore
    {
        public async static Task InsertCounter(Counter counter)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Counter>().Where(x => x.Id == counter.Id).FirstOrDefaultAsync();
            if (collectionItem != null)
            {
                if (collectionItem.MenuVersion != counter.MenuVersion)
                {
                    await sqlConnection.UpdateAsync(counter);
                }
            }
            else
            {
                await sqlConnection.InsertAsync(counter);
            }
            await CounterSubscribed();
        }
        public async static Task<List<Counter>> GetCounters()
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collection = await sqlConnection.Table<Counter>().ToListAsync();
            return collection;
        }

        public async static Task<int> CounterSubscribed()
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            int counters = await sqlConnection.Table<Counter>().CountAsync();
            AppStore.AddValue(Constants.CountersSubscribed, counters.ToString());
            return counters;
        }

        public async static Task<bool> CounterExists(int counterId)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Counter>().Where(x => x.Id == counterId).FirstOrDefaultAsync();
            if (collectionItem != null)
            {
                return true;
            }
            return false;
        }

        public async static Task DeleteCounter(int counterId)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Counter>().Where(x => x.Id == counterId).FirstOrDefaultAsync();
            if (collectionItem != null)
            {
                await sqlConnection.DeleteAsync(collectionItem);
            }
            await CounterSubscribed();
        }
    }
}
