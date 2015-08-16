using SQLite;
using System;

namespace Food_Menu.Storage.Models
{
    [Table("Menu")]
    public class Menu
    {
        [PrimaryKey]
        public int MenuId { get; set; }
        public int CounterId { get; set; }
        public string Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string MenuType { get; set; }
        public double Cost { get; set; }
        public int Version { get; set; }
    }
}
