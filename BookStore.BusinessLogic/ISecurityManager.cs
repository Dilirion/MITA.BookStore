using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.BusinessLogic
{
    public interface ISecurityManager
    {
        bool AuthorizeUser(GetUserModel user, string password);

        GetUserModel GetCurrentUser();

        Task<bool> IsInRoleAsync(string role);
    }
}
