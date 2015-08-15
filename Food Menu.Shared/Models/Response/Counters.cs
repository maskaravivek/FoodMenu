using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Menu.Models.Response
{
    public class Counter
    {
        [JsonProperty(PropertyName = "counter_id")]
        public int CounterId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string CounterName { get; set; }

        [JsonProperty(PropertyName = "menu_version")]
        public int MenuVersion { get; set; }
    }

    public class Counters
    {
        [JsonProperty(PropertyName = "organizationId")]
        public int OrganizationId { get; set; }

        [JsonProperty(PropertyName = "organizationName")]
        public string OrganizationName { get; set; }

        [JsonProperty(PropertyName = "organizationCity")]
        public string OrganizationCity { get; set; }

        [JsonProperty(PropertyName = "counters")]
        public List<Counter> counters { get; set; }
    }
}
