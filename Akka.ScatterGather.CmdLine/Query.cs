namespace Akka.ScatterGather.CmdLine
{
    public class Query
    {
        public Query(string country, string city)
        {
            Country = country;
            City = city;
        }

        public string Country { get; }

        public string City { get; }
    }
}