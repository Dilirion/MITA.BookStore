using System.ComponentModel.Composition;
using IkitMita.Mvvm.Views;

namespace BookStore
{
    
    [Export("CreateOrderView", typeof(IView))]
    public partial class CreateOrderView : IView
    {
        public CreateOrderView()
        {
            InitializeComponent();
        }
    }
}
