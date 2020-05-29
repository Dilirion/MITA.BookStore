using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DataAccess.EF.Models;
using BookStore.DataAccess.Models;

namespace BookStore.DataAccess.EF.Operations
{
    [Export(typeof(ISaveOrderOperation))]
    public class SaveOrderOperation : ISaveOrderOperation
    {
        public async Task<bool> ExecuteAsync(SaveOrderModel orderModel)
        {
            using (var db = new BookStoreDbContext())
            {
                var order = new Order
                {
                    ClientId = orderModel.ClientId,
                    EmployeeId = orderModel.EmployeeId,
                    OrderDate = orderModel.OrderDate,
                    TotalConst = orderModel.TotalCost
                };
                db.Orders.Add(order);

                foreach (var bookModel in orderModel.OrderedBooks)
                {
                    db.OrderedBooks.Add(
                        new OrderedBook
                        {
                            Amount = bookModel.Amount,
                            BookId = bookModel.BookId,
                            Order = order,
                            Price = bookModel.Price
                        });
                }

                var bookIds = orderModel.OrderedBooks.Select(ob => ob.BookId).ToArray();

                var bookAmounts = await db.BookAmounts
                    .Where(ba => ba.BranchId == orderModel.BranchId)
                    .Where(ba => bookIds.Contains(ba.BookId))
                    .ToListAsync();

                foreach (var bookAmount in bookAmounts)
                {
                    var orderAmount = orderModel.OrderedBooks
                        .First(ob => ob.BookId == bookAmount.BookId)
                        .Amount;
                    bookAmount.Amount -= orderAmount;
                }

                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
