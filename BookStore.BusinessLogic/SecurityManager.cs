using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.BusinessLogic
{
    [Export(typeof(ISecurityManager))]
    class SecurityManager: ISecurityManager
    {
        private GetUserModel _authUser;

        public bool AuthorizeUser(GetUserModel user, string password)
        {
            try
            {
                //var isValid = PasswordManager.ValidatePassword(password, user.Password);

                if (user != null)
                {
                    _authUser = user;
                    return true;
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }

            return false;
        }

        public GetUserModel GetCurrentUser()
        {
            if (_authUser == null)
            {
                throw new InvalidOperationException("Пользователь не авторизован");
            }

            return _authUser;
        }

        public Task<bool> IsInRoleAsync(string role)
        {
            throw new NotImplementedException();
        }
    }
}
