using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess
{
    public interface IGetUserOperation
    {
        Task<GetUserModel> ExecuteAsync(string login, string password);
    }
}