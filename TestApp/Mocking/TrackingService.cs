using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NGeoHash;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestApp.Mocking
{
    public interface IFileReader
    {
        string ReadAllText(string path);        
    }

    public class SystemFileReader : IFileReader
    {
        public string ReadAllText(string path) => File.ReadAllText(path);
    }

    // dotnet add package NGeoHash
    public class TrackingService
    {
        private readonly IFileReader fileReader;

        public TrackingService(IFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public TrackingService()
            : this(new SystemFileReader())
        {

        }

        public Location Get()
        {
            string json = fileReader.ReadAllText("tracking.txt");

            try
            {
                Location location = JsonConvert.DeserializeObject<Location>(json);

                if (location == null)
                    throw new ApplicationException("Error parsing the location");

                return location;
            }
            catch(JsonReaderException)
            {
                throw new ApplicationException();
            }

            
        }


        // geohash.org
        public string GetPathAsGeoHash()
        {
            IList<string> path = new List<string>();

            using (var context = new TrackingContext())
            {
                var locations = context.Trackings.Where(t=>t.ValidGPS).Select(t=>t.Location).ToList();

                foreach (Location location in locations)
                {
                    path.Add(GeoHash.Encode(location.Latitude, location.Longitude));
                }

                return string.Join(",", path);
                    
            }
        }
    }


    public class TrackingContext : DbContext
    {
        public DbSet<Tracking> Trackings { get; set; }
    }

    public class Location
    {
        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{Latitude} {Longitude}";
        }

    }

    public class Tracking
    {
        public Location Location { get; set; }
        public byte Satellites { get; set; }
        public bool ValidGPS { get; set; }
    }


}