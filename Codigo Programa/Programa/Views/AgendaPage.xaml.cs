using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using Applicacion.Models;
using Applicacion.Services;
using Programa.Contracts.Views;
using Programa.Core.Contracts.Services;
using Programa.Core.Models;

namespace Programa.Views
{
    public partial class AgendaPage : Page, INotifyPropertyChanged, INavigationAware
    {
        private readonly ISampleDataService _sampleDataService;

        private SampleOrder _selected;
        private Tipo_Persona _tipo_persona;
        private Personas sel_Persona;
        private Paises _pais;
        private Paises _pais2;
        private List<Paises> _lspaises;
        private Database db = new Database();

        public SampleOrder Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public AgendaPage(ISampleDataService sampleDataService)
        {
            _sampleDataService = sampleDataService;
            InitializeComponent();
            DataContext = this;
            List<Personas> ls = db.getPersonas();
            _lspaises = db.getPaises();
            personasList.ItemsSource = ls;
            paisesList.ItemsSource = _lspaises;
            paisesList2.ItemsSource = _lspaises;
        }

        public async void OnNavigatedTo(object parameter)
        {
            SampleItems.Clear();

            var data = await _sampleDataService.GetListDetailsDataAsync();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }

            Selected = SampleItems.First();
        }

        public void OnNavigatedFrom()
        {
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

        private void ShowCreatePanel(object sender, System.Windows.RoutedEventArgs e)
        {
            nuevo.Visibility = System.Windows.Visibility.Visible;
            vista.Visibility = System.Windows.Visibility.Collapsed;
            tipo_persona.ItemsSource = db.getTipo_Persona();
        }

        private void saveCreate(object sender, System.Windows.RoutedEventArgs e)
        {
            if(_pais != null && _tipo_persona != null)
            {
                int pais = _pais.id_pais;
                int tipo = _tipo_persona.id_tipo;
                db.createPersona(identificacion.Text, primerNombre.Text, segundoNombre.Text, primerApellido.Text, segundoApellido.Text, pais, tipo);
                personasList.ItemsSource = null;
                personasList.ItemsSource = db.getPersonas();
                nuevo.Visibility = System.Windows.Visibility.Collapsed;                
            }            
        }

        private void paisSeleccionCrear(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null)  _pais = (Paises)e.AddedItems[0];
        }
        private void paisSeleccion2(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null) _pais2 = (Paises)e.AddedItems[0];
        }
        private void tipoSeleccionCrear(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null) _tipo_persona = (Tipo_Persona)e.AddedItems[0];
        }

        private void personasList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0] != null)
            {
                sel_Persona = (Personas)e.AddedItems[0];
                nuevo.Visibility = System.Windows.Visibility.Collapsed;
                vista.Visibility = System.Windows.Visibility.Visible;

                viewid.Text = sel_Persona.identificacion;
                viewpn.Text = sel_Persona.primer_nombre;
                viewsn.Text = sel_Persona.segundo_nombre;
                viewpa.Text = sel_Persona.primer_apellido;
                viewsa.Text = sel_Persona.segundo_apellido;
                paisdeorigen.Text = "Pais de origen: " + db.getPais(sel_Persona.id_pais_origen).nombre_pais;

                TelefonosView.ItemsSource = db.getPersonaTelefonos(sel_Persona.id_persona);
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NewPhone.Visibility = System.Windows.Visibility.Visible;
            TelefonosView.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void viewTelefonos(object sender, System.Windows.RoutedEventArgs e)
        {
            NewPhone.Visibility = System.Windows.Visibility.Collapsed;
            TelefonosView.Visibility = System.Windows.Visibility.Visible;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void GuardarNumero(object sender, System.Windows.RoutedEventArgs e)
        {
            if(_pais2 != null && Pnumero.Text.Length > 5 && sel_Persona != null)
            {
                db.createTelefono(Pnumero.Text, _pais2.id_pais, sel_Persona.id_persona);
                TelefonosView.ItemsSource = db.getPersonaTelefonos(sel_Persona.id_persona);
                Pnumero.Text = "";
                viewTelefonos(null, null);
            }
        }
    }
}
