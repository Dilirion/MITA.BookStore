using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess.WebApi.Operations
{
    [Export(typeof(ISearchBooksOperation))]
    class SearchBooksOperation : WebApiClient, ISearchBooksOperation
    {
        public async Task<ICollection<SearchBookModel>> ExecuteAsync(string searchString, int branchId)
        {
            try
            {
                var url = $"books?branchId={branchId}&searchString={searchString}";
                return await GetAsync<List<SearchBookModel>>(url);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return null;
            }
        }
    }
}
