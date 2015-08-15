using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Food_Menu.Storage.Models
{
    [Table("Organization")]
    public class Organization
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}
