using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Food_Menu.Models;

namespace Food_Menu.Services
{
    public class MenuService
    {
        public async static Task<ResponseData> GetMenu(int counter, int version)
        {
            ResponseData responseData = await ConnectionManager.SendRequestPacket<GetMenuRequest>("getMenu.php", new GetMenuRequest(counter, version));
            return responseData;
        }
    }
}
