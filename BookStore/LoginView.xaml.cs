using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using BookStore.ViewModels;
using IkitMita.Mvvm.Views;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    
    [Export("LoginView", typeof(IView))]

    public partial class LoginView : IView
    {
        public LoginView()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var newVm = e.NewValue as LoginViewModel;
            var oldVm = e.OldValue as LoginViewModel;
            if (oldVm != null)
            {
                oldVm.PropertyChanged -= OnViewModelPropertyChanged;
            }
            if (newVm != null)
            {
                newVm.PropertyChanged += OnViewModelPropertyChanged;
            }

            OnViewModelPropertyChanged(newVm, new PropertyChangedEventArgs(nameof(LoginViewModel.Password)));
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var passwordBox = (PasswordBox)PasswordControl.Control;
            if (LoginViewModel != null &&
                e.PropertyName == nameof(LoginViewModel.Password) &&
                passwordBox.Password != LoginViewModel.Password)
            {
                passwordBox.Password = LoginViewModel.Password;
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (LoginViewModel != null && LoginViewModel.Password != ((PasswordBox)sender).Password)
            {
                LoginViewModel.Password = ((PasswordBox) sender).Password;
            }
        }

        private LoginViewModel LoginViewModel => DataContext as LoginViewModel;
    }
}
