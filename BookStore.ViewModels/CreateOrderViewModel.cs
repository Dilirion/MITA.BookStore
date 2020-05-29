using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using BookStore.BusinessLogic;
using BookStore.DataAccess;
using BookStore.DataAccess.Models;
using IkitMita;
using IkitMita.Mvvm.ViewModels;

namespace BookStore.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CreateOrderViewModel : ChildViewModelBase
    {
        private GetEmployeeModel _currentEmployee;
        private DelegateCommand<string> _searchBooksCommand;
        private ICollection<GetClientModel> _clients; 
        private ICollection<SearchBookModel> _foundBooks;
        private ObservableCollection<SaveOrderedBookModel> _orderedBooks;
        private DelegateCommand<SearchBookModel> _selectBooksCommand;
        private DelegateCommand<SaveOrderedBookModel> _unselectBookCommand;
        private DelegateCommand _saveOrderCommand;
        private string _errorMessage;

        public CreateOrderViewModel()
        {
            Title = "Создание заказа";
        }

        public ICollection<GetClientModel> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public GetClientModel SelectedClient { get; set; }

        public ICollection<SearchBookModel> FoundBooks
        {
            get { return _foundBooks; }
            set
            {
                _foundBooks = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SaveOrderedBookModel> OrderedBooks
        {
            get { return _orderedBooks; }
            set
            {
                _orderedBooks = value;
                OnPropertyChanged();
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public async void InitializeAsync()
        {
            using (StartOperation())
            {
                
                Clients = await GetClientsOperation.ExecuteAsync();
                _currentEmployee = await GetEmployeeOpertion.ExecuteAsync(SecurityManager.GetCurrentUser().Id);
                OrderedBooks = new ObservableCollection<SaveOrderedBookModel>();
            }
        }

        public DelegateCommand<string> SearchBooksCoomand
            => _searchBooksCommand ?? (_searchBooksCommand = new DelegateCommand<string>(SearchBooksAsync));

        public DelegateCommand<SearchBookModel> SelectBookCommand
            => _selectBooksCommand ?? (_selectBooksCommand = new DelegateCommand<SearchBookModel>(SelectBook));

        public DelegateCommand<SaveOrderedBookModel> UnselectBookCommand
            => _unselectBookCommand ??
            (_unselectBookCommand = new DelegateCommand<SaveOrderedBookModel>(bm => OrderedBooks.Remove(bm)));

        public DelegateCommand SaveOrderCommand => _saveOrderCommand 
            ?? (_saveOrderCommand = new DelegateCommand(SaveOrderAsync));

        private async void SaveOrderAsync()
        {
            if (SelectedClient == null)
            {
                ErrorMessage = "Не выбран клиент!";
                return;
            }
            if (OrderedBooks.IsNullOrEmpty())
            {
                ErrorMessage = "Необходимо добавить хотя бы одну книгу.";
                return;
            }
            using (StartOperation())
            {
                var saveOrderModel = new SaveOrderModel
                {
                    OrderedBooks = OrderedBooks,
                    BranchId = _currentEmployee.BranchId,
                    ClientId = SelectedClient.Id,
                    EmployeeId = _currentEmployee.Id,
                    OrderDate = DateTime.Now
                };

                await SaveOrderOperation.ExecuteAsync(saveOrderModel);
                await Close(true);
            }
        }


        public async void SearchBooksAsync(string searchString)
        {
            using (StartOperation())
            {
                FoundBooks = await SearchBookOperation.ExecuteAsync(searchString, _currentEmployee.BranchId);
            }
        }

        public void SelectBook(SearchBookModel bookModel)
        {
            var saveOrderedBookModel = OrderedBooks.FirstOrDefault(ob => ob.BookId == bookModel.Id);
            if (saveOrderedBookModel == null)
            {
                saveOrderedBookModel = new SaveOrderedBookModel
                {
                    BookId = bookModel.Id,
                    BookTitle = bookModel.BookTitle,
                    Amount = 0,
                    Price = bookModel.Price,
                    MaxAmount = bookModel.Amount
                };
                OrderedBooks.Add(saveOrderedBookModel);
            }
            saveOrderedBookModel.Amount += 1;
        }

        [Import]
        private ISearchBooksOperation SearchBookOperation { get; set; }

        [Import]
        private IGetClientsOperation GetClientsOperation { get; set; }

        [Import]
        private IGetEmployeeOperation GetEmployeeOpertion { get; set; }

        [Import]
        private ISecurityManager SecurityManager { get; set; }

        [Import]
        private ISaveOrderOperation SaveOrderOperation { get; set; }
    }
}
