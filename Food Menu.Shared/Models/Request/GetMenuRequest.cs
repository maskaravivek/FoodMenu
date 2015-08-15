using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Models
{
    [DataContract]
    public class GetMenuRequest
    {
        [DataMember(Name = "counter")]
        public string Counter { get; set; }

        [DataMember(Name = "previous_version")]
        public string PreviousVersion { get; set; }

        public GetMenuRequest(string counter, string version)
        {
            Counter = counter;
            PreviousVersion = version;
        }
    }
}
