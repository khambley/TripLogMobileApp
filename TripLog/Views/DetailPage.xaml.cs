using System;
using System.Collections.Generic;
using TripLog.Models;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TripLog.Views
{
    public partial class DetailPage : ContentPage
    {
        DetailViewModel ViewModel => BindingContext as DetailViewModel;
        public DetailPage(TripLogEntry entry)
        {
            InitializeComponent();
            BindingContext = new DetailViewModel(entry);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(
                    ViewModel.Entry.Latitude,
                    ViewModel.Entry.Longitude),
                    Distance.FromMiles(.5)));

            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = entry.Title,
                Position = new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude)
            });

        }
    }
}

