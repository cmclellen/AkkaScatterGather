namespace Akka.ScatterGather.CmdLine.Domain
{
    public class Hotel
    {
        public Hotel(string country, string city, string name)
        {
            Country = country;
            City = city;
            Name = name;
        }

        public string Country { get; }

        public string City { get; }

        public string Name { get; }

        public override string ToString()
        {
            return $"[Hotel: Country={Country}, City={City}, Name={Name}]";
        }
    }
}