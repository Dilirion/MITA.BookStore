using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess.WebApi.Operations
{
    [Export(typeof(ISaveOrderOperation))]
    class SaveOrderOperation : WebApiClient, ISaveOrderOperation
    {
        public async Task<bool> ExecuteAsync(SaveOrderModel orderModel)
        {
            try
            {
                var result = await PostAsync<bool>("orders", orderModel);
                return result;
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }

            return false;
        }
    }
}
