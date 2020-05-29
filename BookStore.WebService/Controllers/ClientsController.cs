using System.Collections.Generic;
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
    public class ClientsController : ApiController
    {
        [Import]
        private IGetClientsOperation GetClientsOperation { get; set; }

        public async Task<ICollection<GetClientModel>> GetClientsAsync(string clientLastName)
        {
            return await GetClientsOperation.ExecuteAsync(clientLastName);
        }
    }
}