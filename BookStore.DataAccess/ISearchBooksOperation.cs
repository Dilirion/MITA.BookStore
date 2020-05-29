using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess
{
    public interface ISearchBooksOperation
    {
        Task<ICollection<SearchBookModel>> ExecuteAsync(string searchString, int branchId);
    }
}
