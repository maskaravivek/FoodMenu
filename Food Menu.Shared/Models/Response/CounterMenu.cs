using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Models.Response
{
    public class FoodItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class FoodMenu
    {
        [JsonProperty("menu_id")]
        public int MenuId { get; set; }

        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("start_time")]
        public string StartTime { get; set; }

        [JsonProperty("end_time")]
        public string EndTime { get; set; }

        [JsonProperty("type")]
        public string MenuType { get; set; }

        [JsonProperty("cost")]
        public double Cost { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }
        
        [JsonProperty("items")]
        public List<FoodItem> FoodItems { get; set; }
    }

    public class CounterMenu
    {
        [JsonProperty("counter_id")]
        public int CounterId { get; set; }

        [JsonProperty("menu")]
        public List<FoodMenu> menu { get; set; }
    }
}
