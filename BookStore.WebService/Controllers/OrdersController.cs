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
    public class OrdersController : ApiController
    {
        [Import]
        private ISaveOrderOperation SaveOrderOperation { get; set; }

        public async Task<bool> PostOrderAsync(SaveOrderModel orderModel)
        {
            return await SaveOrderOperation.ExecuteAsync(orderModel);
        } 
    }
}