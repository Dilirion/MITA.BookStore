using System;
using System.Collections.Generic;
using IkitMita.DataAccess;

namespace BookStore.DataAccess.EF.Models
{
    public class Author : FullNamedDomainObject
    {
        public DateTime? Birthday { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
