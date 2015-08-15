using Food_Menu.Storage.Models;
using Food_Menu.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Storage
{
    public class OrganizationStore
    {
        public async static Task InsertOrganization(Organization organization)
        {
            SQLiteAsyncConnection sqlConnection = new SQLiteAsyncConnection(DbHelper.DB_PATH);
            var collectionItem = await sqlConnection.Table<Organization>().Where(x => x.Id == organization.Id).FirstOrDefaultAsync();
            if (collectionItem == null)
            {
                await sqlConnection.InsertAsync(organization);
            }
        }
    }
}
