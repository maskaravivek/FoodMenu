using Food_Menu.Storage.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Food_Menu.Utils
{
    public class DbHelper
    {
        public const string DB_NAME = "FoodMenu.db";
        public static readonly string DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_NAME));
        public const int DEFAULT_LIMIT_NUM_MERCHANT_RECORDS_TO_FETCH = 100;

        public async static void CheckAndCreateDatabase()
        {
            if (!await FileUtils.CheckFileExists(DbHelper.DB_NAME))
            {
                System.Diagnostics.Debug.WriteLine("creating tables");
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(DB_PATH);
                await conn.CreateTableAsync<Organization>();
                await conn.CreateTableAsync<Counter>();
                await conn.CreateTableAsync<Menu>();
                await conn.CreateTableAsync<Item>();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("tables exist");
            }
        }
    }
}
