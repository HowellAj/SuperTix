namespace SuperTix.Models
{
    public class Ticket
    {
        //PK
        public int TicketID { get; set; }
        //FK
        public int EventID { get; set; }

        public DateTime EventDate { get; set; }

        public string EventName { get; set; }

        public string TicketHolderName { get; set; }

        public string Address { get; set; }
     
        public decimal Price { get; set; }

        public string SeatNumber { get; set; }

        public Boolean IsAvailable { get; set; }







    }
}
