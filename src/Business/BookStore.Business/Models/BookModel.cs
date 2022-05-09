
namespace BookStore.Business.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }


        public override string ToString()
        {
            return $"Id: {Id} - Name: {Name} - Author: {Author} - Price: {Price}";
        }
    }
}