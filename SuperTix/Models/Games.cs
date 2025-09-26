namespace SuperTix.Models
{
    public class Games
    {
        //PK 
        public int GameID { get; set; }

        //FK
        public int TicketID { get; set; }

        public string GameName { get; set; }

        public string GameDescription { get; set; } = string.Empty;

        public string GameType { get; set; } = string.Empty;

        public DateTime GameDate { get; set; }

        public string GameLocation { get; set; } = string.Empty;

        //Navigator
        public List<Ticket>? Tickets { get; set; }
    }
}
