using System.Linq;
using System.Threading.Tasks;
using Tollminder.Core.Helpers;
using Tollminder.Core.Services.Implementation;

namespace Tollminder.Core.Models.Statuses
{
	public class NotOnTollRoadStatus : BaseStatus 
	{
		public override Task<TollGeolocationStatus> CheckStatus ()
		{
			Log.LogMessage (string.Format ($"TRY TO FIND TOLLPOINT ENTRANCES FROM {SettingsService.WaypointLargeRadius * 1000} m"));

			var location = GeoWatcher.Location;
            var waypoints = GeoDataService.FindNearestEntranceTollPoints(location);

            WaypointChecker.SetTollPointsInRadius(waypoints);
            WaypointChecker.SetIgnoredChoiceTollPoint(null);

            if (waypoints.Count == 0)
            {
                GeoWatcher.StopUpdatingHighAccuracyLocation();
                Log.LogMessage($"No waypoint founded for location {GeoWatcher.Location}");
                return Task.FromResult(TollGeolocationStatus.NotOnTollRoad);
            }
            else
            {
                foreach(var item in WaypointChecker.TollPointsInRadius)
                    Log.LogMessage($"FOUNDED WAYPOINT : {item.Name}, DISTANCE {item.Distance}");

                GeoWatcher.StartUpdatingHighAccuracyLocation();

                return Task.FromResult(TollGeolocationStatus.NearTollRoadEntrance);
            }
		}

        public override bool CheckBatteryDrain()
        {
            var distance = DistanceChecker.GetMostClosestTollPoint(GeoWatcher.Location, GeoDataService.GetAllEntranceTollPoints()).FirstOrDefault()?.Distance ?? 0;
            return BatteryDrainService.CheckGpsTrackingSleepTime(distance);
        }
	}
}

