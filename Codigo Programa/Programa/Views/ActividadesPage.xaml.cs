using Applicacion.Models;
using Applicacion.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace Programa.Views
{
    public partial class ActividadesPage : Page, INotifyPropertyChanged
    {
        private Database db = new Database();
        private Personas persona;
        private string registro;
        public ActividadesPage()
        {
            InitializeComponent();
            DataContext = this;
            controlView.ItemsSource = db.getActividades();
            Personas.ItemsSource = db.getPersonas();
            List<string> ls = new List<string>();
            ls.Add("ENTRADA");
            ls.Add("SALIDA");
            opt.ItemsSource = ls;
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
        private void persona_Selection(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null) persona = (Personas)e.AddedItems[0];
        }
        private void action_Selection(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null) registro = (string)e.AddedItems[0];
        }
        private void GuardarRegistro(object sender, System.Windows.RoutedEventArgs e)
        {
            if (persona != null && registro != null)
            {
                db.insertarRegistro(persona.id_persona, registro);
                controlView.ItemsSource = null;
                controlView.ItemsSource = db.getActividades();
            }
            
        }
    }
}
