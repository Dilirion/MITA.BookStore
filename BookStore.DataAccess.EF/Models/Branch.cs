using IkitMita.DataAccess;

namespace BookStore.DataAccess.EF.Models
{
    public class Branch : TitledDomainObject
    {
        public string Address { get; set; }
    }
}
