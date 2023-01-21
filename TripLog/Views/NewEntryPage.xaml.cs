﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;

namespace TripLog.Views
{
    public partial class NewEntryPage : ContentPage
    {
        NewEntryViewModel ViewModel => BindingContext as NewEntryViewModel;

        public NewEntryPage()
        { 
            InitializeComponent();
            BindingContextChanged += Page_BindingContextChanged;
            //BindingContext = new NewEntryViewModel(DependencyService.Get<INavService>());
        }
        void Page_BindingContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }
        void ViewModel_ErrorsChanged(object sender,
        DataErrorsChangedEventArgs e)
        {
            var propHasErrors = (ViewModel.GetErrors(e.PropertyName)
                as List<string>)?.Any() == true;
            switch (e.PropertyName)
            {
                case nameof(ViewModel.Title):
                    title.LabelColor = propHasErrors
                        ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.Rating):
                    rating.LabelColor = propHasErrors
                        ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}

