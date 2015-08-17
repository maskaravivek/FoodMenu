using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Food_Menu.Models.Request
{
    [DataContract]
    public class AddCounterRequest
    {
        [DataMember(Name = "organization")]
        public string OrganizationId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public AddCounterRequest(string name, string org_id)
        {
            OrganizationId = org_id;
            Name = name;
        }
    }
}
