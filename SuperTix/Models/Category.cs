namespace SuperTix.Models

{
    public class Category
    {
        //PK
        public int CategoryId { get; set; } 

        public string? CategoryName { get; set; }

        public List<Game>? Games { get; set; }
    }
}
