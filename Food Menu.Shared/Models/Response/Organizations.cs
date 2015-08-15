using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Menu.Models.Response
{
    public class Organization
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string OrganizationName { get; set; }
    }

    public class Organizations
    {
        public List<Organization> organizations { get; set; }
    }
}
