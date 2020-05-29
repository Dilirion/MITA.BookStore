using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess
{
    public interface IGetEmployeeOperation
    {
        Task<GetEmployeeModel> ExecuteAsync(string userLogin);
    }
}