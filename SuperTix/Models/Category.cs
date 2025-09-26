namespace SuperTix.Models
{
    public class Category
    {
        //PK 
        public int CategoryID { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        //Navigator
        public List<Game>? Games { get; set; }

    };
}