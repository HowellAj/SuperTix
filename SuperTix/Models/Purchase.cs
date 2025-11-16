using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperTix.Models
{
    public class Purchase
    {
     
        public int PurchaseId { get; set; }
    
        public int GameId { get; set; }

        public Game Game { get; set; }
      
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public int TicketCount { get; set; }
       
        public string CreditCardLast4 { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.Now;



    }
}
