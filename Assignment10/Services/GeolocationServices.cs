using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment10.Services
{
    public static class GeolocationServices
    {
        public static async Task<Location> GetCurrentLocationAsync(int timeoutSeconds = 10)
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if ( status != PermissionStatus.Granted)
             status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        
            if(status != PermissionStatus.Granted)
             return null;

            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds((double)timeoutSeconds));
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds((double)timeoutSeconds + 2));
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location == null)
                {
                    System.Diagnostics.Debug.WriteLine("Location is null - maybe GPS not set or disabled.");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Got location: {location.Latitude}, {location.Longitude}");
                }

                return location;
            }
            catch (FeatureNotEnabledException)
            {
                System.Diagnostics.Debug.WriteLine("FeatureNotEnabledException - location services are off.");
                return null;
            }
            catch (PermissionException)
            {
                System.Diagnostics.Debug.WriteLine("PermissionException - location permission denied.");
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Other Exception: {ex.Message}");
                return null;
            }
        }
    }
}
