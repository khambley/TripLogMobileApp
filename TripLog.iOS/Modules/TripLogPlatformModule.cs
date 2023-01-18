using System;
using TripLog.iOS.Services;
using TripLog.Services;

namespace TripLog.iOS.Modules
{
	public class TripLogPlatformModule : Ninject.Modules.NinjectModule
	{
		public TripLogPlatformModule()
		{
		}

        public override void Load()
        {
            Bind<ILocationService>().To<LocationService>().InSingletonScope();
        }
    }
}

