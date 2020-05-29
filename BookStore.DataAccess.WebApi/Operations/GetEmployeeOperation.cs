using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess.WebApi.Operations
{
    [Export(typeof(IGetEmployeeOperation))]
    public class GetEmployeeOperation : WebApiClient, IGetEmployeeOperation
    {
        public async Task<GetEmployeeModel> ExecuteAsync(string userLogin)
        {
            try
            {
                return await GetAsync<GetEmployeeModel>($"employees/{userLogin}");
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                return null;
            }
        }
    }
}
