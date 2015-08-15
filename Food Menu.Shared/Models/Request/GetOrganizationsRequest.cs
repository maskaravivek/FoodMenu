using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Food_Menu.Models.Request
{
    [DataContract]
    public class GetOrganizationsRequest
    {
        [DataMember(Name = "city")]
        public string City { get; set; }

        public GetOrganizationsRequest(string city)
        {
            City = city;
        }
    }
}
