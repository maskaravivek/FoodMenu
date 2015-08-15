using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Models.Request
{
    [DataContract]
    public class GetCitiesRequest
    {
        [DataMember(Name = "dummy_request")]
        public string Dummy { get; set; }

        public GetCitiesRequest(string someValue)
        {
            Dummy = someValue;
        }
    }
}
