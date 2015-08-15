using SQLite;

namespace Food_Menu.Storage.Models
{
    [Table("Menu")]
    public class Menu
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int CounterId { get; set; }
        public int MenuId { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string MenuType { get; set; }
        public double Cost { get; set; }
        public int Version { get; set; }
    }
}
