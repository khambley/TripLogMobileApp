using System;
using Ninject;
using Ninject.Modules;
using TripLog.Services;
using TripLog.ViewModels;
using TripLog.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TripLog.Modules;

namespace TripLog
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set;}

        public App (params INinjectModule[] platformModules)
        {
            InitializeComponent();

            var navService = DependencyService.Get<INavService>() as XamarinFormsNavService;
            
            // Register core services 
            Kernel = new StandardKernel(
                new TripLogCoreModule(),
                new TripLogNavModule());

            // Register platform specific services 
            Kernel.Load(platformModules);
            SetMainPage();
        }

        private void SetMainPage()
        {
            var mainPage = new NavigationPage(new MainPage())
            {
                BindingContext = Kernel.Get<MainViewModel>()
            };

            var navService = Kernel.Get<INavService>() as XamarinFormsNavService;

            navService.XamarinFormsNav = mainPage.Navigation;

            MainPage = mainPage;
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

