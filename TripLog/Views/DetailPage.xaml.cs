using System;
using System.Collections.Generic;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog.Views
{
    public partial class DetailPage : ContentPage
    {
        DetailViewModel ViewModel => BindingContext as DetailViewModel;
        public DetailPage()
        {
            InitializeComponent();
            BindingContext = new DetailViewModel(DependencyService.Get<INavService>());

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(
                    ViewModel.Entry.Latitude,
                    ViewModel.Entry.Longitude),
                    Distance.FromMiles(.5)));

            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = ViewModel.Entry.Title,
                Position = new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude)
            });

        }
    }
}

