using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Food_Menu.Models;
using Food_Menu.Models.Request;

namespace Food_Menu.Services
{
    public class LocationService
    {
        public static async Task<ResponseData> GetCities()
        {
            ResponseData responseData = await ConnectionManager.SendRequestPacket<GetCitiesRequest>("getCities.php", new GetCitiesRequest("hello world"));
            return responseData;
        }
    }
}
