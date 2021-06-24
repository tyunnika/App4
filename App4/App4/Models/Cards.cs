using System;
using SQLite;

namespace App4.Models
{
   public class Cards
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
