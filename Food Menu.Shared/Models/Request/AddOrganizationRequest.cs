using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Food_Menu.Models.Request
{
    [DataContract]
    public class AddOrganizationRequest
    {
        [DataMember(Name = "organization")]
        public string Organization { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        public AddOrganizationRequest(string name, string city)
        {
            Organization = name;
            City = city;
        }
    }
}
