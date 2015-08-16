using Food_Menu.Storage.Models;
using Food_Menu.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Storage
{
    public class ItemStore
    {
        public async static Task InsertItem(Item item)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            await sqlConnection.InsertAsync(item);
        }

        public async static Task DeleteItems(int menuId)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Item>().Where(x => x.MenuId == menuId).ToListAsync();
            foreach(Item item in collectionItem)
            {
                await sqlConnection.DeleteAsync(item);
            }
        }

        public async static Task<List<Item>> GetItems(int menuId)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collection = await sqlConnection.Table<Item>().Where(x => x.MenuId== menuId).ToListAsync();
            return collection;
        }
    }
}
