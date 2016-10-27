﻿using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Tollminder.Core.Models
{
	[Table("TollRoads")]
    public class TollRoad : IEquatable<TollRoad>
	{
		[PrimaryKey, AutoIncrement]
		public long Id { get; set; }
		public string Name { get; set; }
		[OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<TollRoadWaypoint> WayPoints { get; set; }

        public TollRoad()
        {

        }

        public TollRoad(string name, List<TollPoint> points)
        {
            Name = name;
            WayPoints = new List<TollRoadWaypoint>();
            foreach(var item in points)
            {
                WayPoints.Add(new TollRoadWaypoint()
                {
                    TollPoints = new List<TollPoint>() { item }
                });
            };
        }

        public bool Equals(TollRoad other)
        {
            return Id == other.Id;
        }

        public override string ToString()
        {
            return string.Format("[TollRoad: Id={0}, Name={1}, WayPoints={2}]", Id, Name, WayPoints);
        }
    }
}

