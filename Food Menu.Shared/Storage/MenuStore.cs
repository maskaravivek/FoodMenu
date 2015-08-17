using Food_Menu.Storage.Models;
using Food_Menu.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Storage
{
    public class MenuStore
    {
        public async static Task InsertMenu(Menu menu)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Menu>().Where(x => x.MenuId == menu.MenuId && x.CounterId == menu.CounterId).FirstOrDefaultAsync();
            if (collectionItem != null)
            {
                if (collectionItem.Version < menu.Version)
                {
                    await sqlConnection.UpdateAsync(menu);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Menu Inserted: "+await sqlConnection.InsertAsync(menu));
            }
        }

        public async static Task<Menu> GetMenu(int counterId)
        {
            string currentDay = DateTime.Today.DayOfWeek.ToString();
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Menu>().Where(x => x.CounterId == counterId).FirstOrDefaultAsync();
            return collectionItem;
        }
    }
}
