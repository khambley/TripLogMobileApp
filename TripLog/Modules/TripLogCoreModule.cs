using System;
using TripLog.Services;
using TripLog.ViewModels;

namespace TripLog.Modules
{
	public class TripLogCoreModule : Ninject.Modules.NinjectModule
	{
		public TripLogCoreModule()
		{
		}

        public override void Load()
        {
            // ViewModels 
            Bind<MainViewModel>().ToSelf();
            Bind<DetailViewModel>().ToSelf();
            Bind<NewEntryViewModel>().ToSelf();

            // Core Services
            var tripLogService = new TripLogApiDataService(new Uri("https://mytriplogapi.azurewebsites.net"));
            Bind<ITripLogDataService>()
                .ToMethod(x => tripLogService)
                .InSingletonScope();
        }
    }
}

