namespace SuperTix.Models
{
    public class Concerts
    {
        //PK 
        public int ConcertID { get; set; }

        //FK
        public int TicketID { get; set; }

        public string ConcertName { get; set; }

        public string ConcertDescription { get; set; } = string.Empty;
        public string ConcertType { get; set; } = string.Empty;

        public DateTime ConcertDate { get; set; }

        public string ConcertLocation { get; set; } = string.Empty;

        //Navigator
        public List<Ticket>? Tickets { get; set; }
    }
}
