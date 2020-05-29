using System.Collections.Generic;
using IkitMita.DataAccess;

namespace BookStore.DataAccess.EF.Models
{
    public class Book : TitledDomainObject
    {
        public string Isbn { get; set; }

        public decimal Price { get; set; }

        public ICollection<Author> Authors { get; set; }

        public int PublishYear { get; set; }
    }
}
