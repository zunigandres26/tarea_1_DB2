using Applicacion.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Applicacion.Views
{
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        private Database db;
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            db = new Database();
            db.ReadData("SELECT * FROM Paises");
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
