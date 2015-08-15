﻿using Food_Menu.Storage.Models;
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
        }

        public async static Task<bool> CounterExists(int counter_id)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Counter>().Where(x => x.Id == counter_id).FirstOrDefaultAsync();
            if (collectionItem != null)
            {
                return true;
            }
            return false;
        }

        public async static Task DeleteCounter(int counter_id)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Counter>().Where(x => x.Id == counter_id).FirstOrDefaultAsync();
            if (collectionItem != null)
            {
                await sqlConnection.DeleteAsync(collectionItem);
            }
        }
    }
}