﻿using System;
using System.Linq;
using Tollminder.Core.Models;
using System.Collections.Generic; 
using Tollminder.Core.Helpers;
using System.Threading.Tasks;
using static Tollminder.Core.Helpers.LocationChecker;

namespace Tollminder.Core.Services.Implementation
{
	public class DistanceChecker : IDistanceChecker
	{
		public double Epsilon { get; } = double.Epsilon;

		private readonly IGeoLocationWatcher _geoWatcher;
		private readonly IWaypointChecker _waypointChecker;

		public DistanceChecker (IGeoLocationWatcher geoWatcher, IWaypointChecker waypointChecker)
		{
			this._geoWatcher = geoWatcher;
			this._waypointChecker = waypointChecker;
		}

		public virtual TollRoadWaypointWithDistance GetMostClosestWaypoint (GeoLocation center, IList<TollRoadWaypoint> points)
		{
			var pts = points.AsParallel().AsOrdered().WithMergeOptions(ParallelMergeOptions.FullyBuffered).OrderBy(x => DistanceBetweenGeoLocations(center, x.Location));

			foreach(var x in pts)
			{
				var distance = DistanceBetweenGeoLocations(center, x.Location);
				Log.LogMessage($"{distance:0.##} {x.Name} {x.Location}");
			}

			TollRoadWaypoint point =  pts?.FirstOrDefault ();

			if (point != null)
			{
				var tollRoadWaypointWithDistance = new TollRoadWaypointWithDistance(point);
				tollRoadWaypointWithDistance.Distance = DistanceBetweenGeoLocations(center, point.Location);
				return tollRoadWaypointWithDistance;
			}

			return null;
		}

        public virtual ParallelQuery<TollRoadWaypoint> GetLocationsFromRadius (GeoLocation center, IList<TollRoadWaypoint> points)
		{
			return points.AsParallel ().WithMergeOptions (ParallelMergeOptions.AutoBuffered).Where (x => (DistanceBetweenGeoLocations (center, x.Location) - SettingsService.MinimumDistanceToWaypoint) < Epsilon);
		}

		public virtual TollRoadWaypoint GetLocationFromRadius (GeoLocation center, IList<TollRoadWaypoint> points)
		{
			return points.AsParallel ().WithMergeOptions (ParallelMergeOptions.AutoBuffered).FirstOrDefault (x => (DistanceBetweenGeoLocations (center, x.Location) - SettingsService.MinimumDistanceToWaypoint) < Epsilon);
		}
	}
}