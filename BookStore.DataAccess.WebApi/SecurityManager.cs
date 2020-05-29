using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Threading.Tasks;
using BookStore.BusinessLogic;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess.WebApi
{
    [Export(typeof(ISecurityManager))]
    [Export(typeof(IAuthTokenProvider))]
    class SecurityManager: ISecurityManager, IAuthTokenProvider
    {
        private GetUserModel _authUser;

        public bool AuthorizeUser(GetUserModel user, string password)
        {
            try
            {
                var isValid = PasswordManager.ValidatePassword(password, user.Password);

                if (isValid)
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

        public string ProvideAuthToken()
        {
            return _authUser?.Password;
        }
    }
}
