using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess.WebApi.Operations
{
    [Export(typeof(IGetUserOperation))]
    class GetUserOperation : WebApiClient, IGetUserOperation
    {
        public async Task<GetUserModel> ExecuteAsync(string login, string password)
        {
            using (var client = new HttpClient())
            {
                var responceMessage = await client.
                    PostAsync("/token", new FormUrlEncodedContent(
                        new Dictionary<string, string>
                {
                    ["username"] = login,
                    ["password"] = password,
                    ["grant_type"] = "password"
                }));

                if (!responceMessage.IsSuccessStatusCode)
                {
                    return null;
                }

                var authModel = await responceMessage.Content.ReadAsAsync<AuthModel>();
                return new GetUserModel
                {
                    Id = authModel.UserName,
                    Login = authModel.UserName,
                    Password = authModel.Token_type + " " + authModel.Access_token
                };
            }
        }
    }
}
