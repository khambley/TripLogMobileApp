using System;
using System.Collections.ObjectModel;
using Akavache;
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

        readonly IBlobCache _cache;

        public MainViewModel(INavService navService, ITripLogDataService tripLogService, IBlobCache cache) : base(navService)
        {
            _tripLogService = tripLogService;
            _cache = cache;
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

        void LoadEntries()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                // Load from local cache and then immediately load from API

                _cache.GetAndFetchLatest("entries", async () => await _tripLogService.GetEntriesAsync())
                    .Subscribe(entries =>
                    {
                        LogEntries = new ObservableCollection<TripLogEntry>(entries);
                        IsBusy = false;
                    });
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}

