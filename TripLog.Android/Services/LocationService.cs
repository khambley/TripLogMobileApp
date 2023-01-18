using System;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;

namespace TripLog.Droid.Services
{
	public class LocationService : ILocationService
	{
		public LocationService()
		{
		}

        public async Task<GeoCoordinates> GetGeoCoordinatesAsync()
        {
            var location = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            return new GeoCoordinates
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
        }
    }
}

