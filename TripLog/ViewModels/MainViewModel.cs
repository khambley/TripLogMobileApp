using System;
using System.Collections.ObjectModel;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        ObservableCollection<TripLogEntry> _logEntries;

        public ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        readonly ITripLogDataService _tripLogService;

        public MainViewModel(INavService navService, ITripLogDataService tripLogService) : base(navService)
        {
            _tripLogService = tripLogService;
            LogEntries = new ObservableCollection<TripLogEntry>();
        }

        public Command<TripLogEntry> ViewCommand => new Command<TripLogEntry>(async entry =>
            await NavService.NavigateTo<DetailViewModel, TripLogEntry>(entry));

        public Command NewCommand => new Command(async () =>
            await NavService.NavigateTo<NewEntryViewModel>());

        public override void Init()
        {
            LoadEntries();
        }

        async void LoadEntries()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var entries = await _tripLogService.GetEntriesAsync();
                LogEntries = new ObservableCollection<TripLogEntry>(entries);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}

