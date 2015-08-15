using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Menu.Storage.Models
{
    [Table("Item")]
    public class Item
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
