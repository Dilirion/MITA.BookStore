using System;
using System.Collections.Generic;
using IkitMita.DataAccess;

namespace BookStore.DataAccess.EF.Models
{
    public class Order : DomainObject
    {
        public int ClientId { get; set; }

        public int EmployeeId { get; set; }

        public decimal TotalConst { get; set; }

        public DateTime OrderDate { get; set; }

        public Client Client { get; set; }

        public Employee Employee { get; set; }

        public ICollection<OrderedBook> Books { get; set; }
    }
}
