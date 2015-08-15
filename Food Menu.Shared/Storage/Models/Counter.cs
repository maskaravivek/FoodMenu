using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Storage.Models
{
    [Table("Counter")]
    public class Counter
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string CounterName { get; set; }
        public int MenuVersion { get; set; }
    }
}
