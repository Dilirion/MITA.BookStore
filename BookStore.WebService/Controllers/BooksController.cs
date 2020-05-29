using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Web.Http;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;

namespace BookStore.WebService.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class BooksController : ApiController
    {
        [Import]
        private  ISearchBooksOperation SearchBooksOperation { get; set; }

        public async Task<ICollection<SearchBookModel>> GetBooksAsync(string searchString, int branchId)
        {
            return await SearchBooksOperation.ExecuteAsync(searchString, branchId);
        }

    }
}