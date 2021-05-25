using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Programa.Views
{
    public partial class ActividadesPage : Page, INotifyPropertyChanged
    {
        public ActividadesPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
