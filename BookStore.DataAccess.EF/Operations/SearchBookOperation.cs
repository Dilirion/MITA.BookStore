using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Models;
using IkitMita.DataAccess;

namespace BookStore.DataAccess.EF.Operations
{
    [Export(typeof(ISearchBooksOperation))]
    internal class SearchBookOperation : ISearchBooksOperation
    {
        public async Task<ICollection<SearchBookModel>> ExecuteAsync(string searchString, int branchId)
        {
            using (var db = new BookStoreDbContext())
            {
                var bookModels = await db.BookAmounts
                    .Where(ba => ba.BranchId == branchId)
                    .Where(ba => ba.Book.Title.Contains(searchString) ||
                                 ba.Book.Isbn.Contains(searchString) ||
                                 ba.Book.Authors.Any(a => a.LastName.Contains(searchString)))
                    .Select(ba => new SearchBookModel
                    {
                        Isbn = ba.Book.Isbn,
                        Authors = ba.Book.Authors.Select(a => new FullNamedDomainObject
                        {
                            LastName = a.LastName,
                            FirstName = a.FirstName,
                            MiddleName = a.MiddleName,
                            Id = a.Id
                        }).ToList(),
                        BookTitle = ba.Book.Title,
                        Amount = ba.Amount,
                        Id = ba.BookId,
                        Price = ba.Book.Price
                    })
                    .ToListAsync();
                return bookModels;
            }
        }
    }
}
