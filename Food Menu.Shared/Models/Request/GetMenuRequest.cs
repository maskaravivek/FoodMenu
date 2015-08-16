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
        public int Counter { get; set; }

        [DataMember(Name = "previous_version")]
        public int PreviousVersion { get; set; }

        public GetMenuRequest(int counter, int version)
        {
            Counter = counter;
            PreviousVersion = version;
        }
    }
}
