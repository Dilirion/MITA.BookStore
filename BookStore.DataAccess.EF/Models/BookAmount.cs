using IkitMita.DataAccess;

namespace BookStore.DataAccess.EF.Models
{
    public class BookAmount : DomainObject
    {
        public int BookId { get; set; }

        public int BranchId { get; set; }

        public int Amount { get; set; }

        public Book Book { get; set; }

        public Branch Branch { get; set; }
    }
}
