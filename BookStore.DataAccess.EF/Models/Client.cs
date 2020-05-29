using System;
using IkitMita.DataAccess;

namespace BookStore.DataAccess.EF.Models
{
    public class Client : FullNamedDomainObject
    {
        public DateTime RegistrationDate { get; set; }
    }
}
