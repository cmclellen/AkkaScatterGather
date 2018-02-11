using System;
using System.Collections.Generic;
using System.Linq;
using Akka.Actor;
using Akka.ScatterGather.CmdLine.Domain;

namespace Akka.ScatterGather.CmdLine.Actors
{
    public class CategoryActor : ReceiveActor
    {
        public CategoryActor(string country, IEnumerable<Hotel> hotels, bool top = false)
        {
            var listOfActors = new List<IActorRef>();
            if (top)
            {
                var countries = hotels.Select(x => x.Country).Distinct().ToList();
                Console.WriteLine($"Countries: [{string.Join("; ", countries)}]");
                
                countries.ForEach(countryName =>
                {
                    var hotelsInCountry = hotels.Where(x => x.Country == countryName).ToList();
                    listOfActors.Add(Context.ActorOf(Props.Create<CategoryActor>(countryName, hotelsInCountry, false), countryName));
                });
            }
            else
            {
                var cities = hotels.Where(x => x.Country == country).Select(x => x.City).Distinct().ToList();
                cities.ForEach(city =>
                {
                    var hotelsInCity = hotels.Where(x => x.Country == country && x.City == city).ToList();
                    listOfActors.Add(Context.ActorOf(Props.Create<TopicActor>(country, city, hotelsInCity), city));
                });
            }

            Receive<Query>(q =>
            {
                var sender = Sender;
                var aggregator = Context.ActorOf(Props.Create<AggregatorActor>(sender, listOfActors.Count),
                    "Aggregator");

                listOfActors.ForEach(x => { x.Tell(q, aggregator); });
            });
        }
    }
}