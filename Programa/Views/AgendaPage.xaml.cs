using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
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
        private Paises _pais;
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
            personasList.ItemsSource = db.getPersonas();
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
            tipo_persona.ItemsSource = db.getTipo_Persona();
            paisesList.ItemsSource = db.getPaises();
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
            _pais = (Paises)e.AddedItems[0];
        }
        private void tipoSeleccionCrear(object sender, SelectionChangedEventArgs e)
        {
            _tipo_persona = (Tipo_Persona)e.AddedItems[0];
        }

        private void personasList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Personas select = (Personas)e.AddedItems[0];
        }
    }
}
