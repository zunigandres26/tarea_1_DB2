﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Programa.Contracts.Services;
using Programa.Contracts.Views;
using Programa.Core.Contracts.Services;
using Programa.Core.Models;

namespace Programa.Views
{
    public partial class MonitoreoPage : Page, INotifyPropertyChanged, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly ISampleDataService _sampleDataService;

        public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public MonitoreoPage(INavigationService navigationService, ISampleDataService sampleDataService)
        {
            _navigationService = navigationService;
            _sampleDataService = sampleDataService;
            InitializeComponent();
            DataContext = this;
        }

        public async void OnNavigatedTo(object parameter)
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await _sampleDataService.GetContentGridDataAsync();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        public void OnNavigatedFrom()
        {
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            => SelectItem(e);

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SelectItem(e);
                e.Handled = true;
            }
        }

        private void SelectItem(RoutedEventArgs args)
        {
            if (args.OriginalSource is FrameworkElement selectedItem
                && selectedItem.DataContext is SampleOrder order)
            {
                _navigationService.NavigateTo(typeof(MonitoreoDetailPage), order.OrderID);
            }
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
