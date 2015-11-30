using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Food_Menu.Models;
using Food_Menu.Models.Request;

namespace Food_Menu.Services
{
    public class CounterService
    {
        public async static Task<ResponseData> GetCountersByOrganization(string organizationId)
        {
            ResponseData responseData = await ConnectionManager.SendRequestPacket<GetCounterRequest>("getCounters.php", new GetCounterRequest(organizationId));
            return responseData;
        }
    }
}
