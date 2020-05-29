using IkitMita.DataAccess;

namespace BookStore.DataAccess.EF.Models
{
    public class OrderedBook : DomainObject
    {
        public int OrderId { get; set; }

        public int BookId { get; set; }

        public  int Amount { get; set; }

        public decimal Price { get; set; }

        public Order Order { get; set; }

        public Book Book { get; set; }
    }
}