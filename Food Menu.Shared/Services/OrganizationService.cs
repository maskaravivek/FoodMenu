using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Food_Menu.Models;
using Food_Menu.Models.Request;

namespace Food_Menu.Services
{
    public class OrganizationService
    {
        public async static Task<ResponseData> GetOrganizationsByCity(string city)
        {
            ResponseData responseData = await ConnectionManager.SendRequestPacket<GetOrganizationsRequest>("getOrganizations.php", new GetOrganizationsRequest(city));
            return responseData;
        }
    }
}
