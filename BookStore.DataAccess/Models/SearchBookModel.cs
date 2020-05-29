using System.Collections.Generic;
using IkitMita.DataAccess;

namespace BookStore.DataAccess.Models
{
    public class SearchBookModel
    {
        public int Id { get; set; }

        public string BookTitle { get; set; }

        public string Isbn { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public ICollection<FullNamedDomainObject> Authors { get; set; } 
    }
}
