using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Models.Response
{
    public class City
    {
        [JsonProperty(PropertyName ="city")]
        public string CityName { get; set; }
    }

    public class Cities
    {
        public List<City> cities { get; set; }
    }
}
