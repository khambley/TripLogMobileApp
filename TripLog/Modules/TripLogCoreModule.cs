using System;
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
        }
    }
}

