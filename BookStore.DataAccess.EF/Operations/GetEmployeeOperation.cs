using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.Models;


namespace BookStore.DataAccess.EF.Operations
{
    [Export(typeof(IGetEmployeeOperation))]
    internal class GetEmployeeOperation : IGetEmployeeOperation
    {
        public async Task<GetEmployeeModel> ExecuteAsync(string userLogin)
        {
            using (var db = new BookStoreDbContext())
            {
                var employee = await db.Employees
                    .Where(e => e.ApplicationUser.UserName == userLogin)
                    .Select(e => new GetEmployeeModel
                    {
                        Id = e.Id,
                        Birthday = e.Birthday,
                        BranchId = e.BranchId,
                        BranchTitle = e.Branch.Title,
                        FireDate = e.FireDate,
                        FirstName = e.FirstName,
                        HireDate = e.HireDate,
                        LastName = e.LastName,
                        MiddleName = e.MiddleName
                    })
                    .FirstOrDefaultAsync();

                return employee;
            }
        }
    }
}
