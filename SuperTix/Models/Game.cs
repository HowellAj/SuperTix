using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperTix.Models
{
    public class Game
    {
        //PK
        public int GameId { get; set; }

        //FK to Category
        public int CategoryId { get; set; }

        public string? GameName { get; set; }

        public string? Description { get; set; }

        public DateTime GameDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string? Owner { get; set; }

        public string? Location { get; set; }

        public Category? Category { get; set; }

        public string? PhotoPath { get; set; }

        [NotMapped]
        [Display(Name = "Photo")]
        public IFormFile? FormFile { get; set; }

    }
}
