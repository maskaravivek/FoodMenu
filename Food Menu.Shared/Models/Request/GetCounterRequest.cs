using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Food_Menu.Models.Request
{
    [DataContract]
    public class GetCounterRequest
    {
        [DataMember(Name = "org_id")]
        public string OrganizationId { get; set; }

        public GetCounterRequest(string org_id)
        {
            OrganizationId = org_id;
        }
    }
}
