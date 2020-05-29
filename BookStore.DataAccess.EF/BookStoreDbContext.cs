using System.Data.Entity;
using BookStore.DataAccess.EF.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookStore.DataAccess.EF
{
    public class BookStoreDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreDbContext() :
            base("BookStoreDbContext", throwIfV1Schema: false)
        { }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAmount> BookAmounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedBook> OrderedBooks { get; set; }
    }
}
