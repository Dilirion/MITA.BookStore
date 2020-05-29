using System.ComponentModel.Composition;
using System.Windows.Input;
using BookStore.DataAccess;
using BookStore.BusinessLogic;
using BookStore.DataAccess.Models;
using IkitMita;
using IkitMita.Mvvm.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace BookStore.ViewModels
{
    [Export]
    public class LoginViewModel : ChildViewModelBase
    {
        private DelegateCommand _makeLoginCommand;
        private string _message;
        private string _login;
        private string _password;

        public LoginViewModel()
        {
            Title = "Авторизация";
#if DEBUG
            Login = "Dilirion";
            Password = "Dilirion";
#endif
        }

        [Import]
        private  IGetUserOperation GetUserOperation { get; set; }

        [Import]
        private IServiceLocator ServiceLocator { get; set; }

        [Import]
        private ISecurityManager SecurityManager { get; set; }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }


        [DependsOn(nameof(Login))]
        [DependsOn(nameof(Password))]
        [DependsOn(nameof(IsFree))]
        public ICommand MakeLoginCommand
        {
            get
            {
                return _makeLoginCommand ??
                       (_makeLoginCommand = new DelegateCommand(MakeLoginAsync,
                           () => !Login.IsNullOrEmpty() && !Password.IsNullOrEmpty() && IsFree));
            }
        }

        private async void MakeLoginAsync()
        {
            using (StartOperation())
            {
                GetUserModel user = await GetUserOperation.ExecuteAsync(Login, Password);

                if (user == null || !SecurityManager.AuthorizeUser(user, Password))
                {
                    Message = "Логин или пароль введены неправильно";
                }
                else
                {
                    var mainViewModel = ServiceLocator.GetInstance<MainViewModel>();
                    mainViewModel.InitializeAsync();
                    mainViewModel.Show();
                    await Close();
                }
            }
        }
    }
}
