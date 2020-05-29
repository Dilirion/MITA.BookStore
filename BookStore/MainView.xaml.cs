using System.ComponentModel.Composition;
using IkitMita.Mvvm.Views;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [Export("MainView",typeof(IView))]
    public partial class MainView : IView
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
