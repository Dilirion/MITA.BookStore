using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess
{
    public interface ISaveOrderOperation
    {
        Task<bool> ExecuteAsync(SaveOrderModel orderModel);
    }
}
