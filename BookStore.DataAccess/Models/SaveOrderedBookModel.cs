using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace BookStore.DataAccess.Models
{
    public class SaveOrderedBookModel: INotifyPropertyChanged
    {
        private int _amount;

        public string BookTitle { get; set; }

        public int BookId { get; set; }

        public decimal Price { get; set; }

        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalCost));
            }
        }

        public decimal TotalCost => Price*Amount;

        public int MaxAmount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
