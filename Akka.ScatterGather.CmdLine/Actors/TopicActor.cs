using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.ScatterGather.CmdLine.Domain;

namespace Akka.ScatterGather.CmdLine.Actors
{
    public class TopicActor : ReceiveActor
    {
        private readonly string _city;
        private readonly string _country;
        private readonly IEnumerable<Hotel> _hotels;

        public TopicActor(string country, string city, IEnumerable<Hotel> hotels)
        {
            this._country = country;
            this._city = city;
            this._hotels = hotels;

            Receive<Query>(q =>
            {
                var sender = Sender;

                sender.Tell(new Results(this._country, this._city,
                    this._hotels));
                    //this._hotels.Where(x => x.Country == country && x.City == city).ToList()));
            });
        }
    }
}