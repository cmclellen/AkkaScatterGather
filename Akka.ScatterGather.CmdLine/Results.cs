using System.Collections.Generic;
using Akka.ScatterGather.CmdLine.Domain;

namespace Akka.ScatterGather.CmdLine
{
    public class Results
    {
        public Results(string country, string city, IEnumerable<Hotel> hotels)
        {
            Country = country;
            City = city;
            Hotels = hotels;
        }

        public string Country { get; }

        public string City { get; }

        public IEnumerable<Hotel> Hotels { get; }
    }
}