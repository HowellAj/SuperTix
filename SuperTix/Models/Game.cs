using System.ComponentModel.DataAnnotations;

namespace SuperTix.Models
{
    public class Game
    {
        //PK 
        public int GameID { get; set; }

        //FK
        public int CategoryID { get; set; }

        public string GameName { get; set; } = string.Empty;

        public string GameDescription { get; set; } = string.Empty;

        public string GameType { get; set; } = string.Empty;

        public DateTime GameDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string Owner { get; set; } = string.Empty;

        public string GameLocation { get; set; } = string.Empty;

        //Navigator
        public Category ? Category { get; set; }
    }
}
