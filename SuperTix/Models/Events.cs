namespace SuperTix.Models
{
    public class Events
    {
        //PK 
        public int EventID { get; set; }

        //FK
        public int TicketID { get; set; }

        public string EventName { get; set; }

        public string EventDescription { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

        public string EventLocation { get; set; } = string.Empty;

        //Navigator
        public List<Ticket>? Tickets { get; set; }
    }
}
