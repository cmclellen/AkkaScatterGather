using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Akka.Actor;
using Akka.ScatterGather.CmdLine.Actors;
using Akka.ScatterGather.CmdLine.Domain;

namespace Akka.ScatterGather.CmdLine
{
    internal class Program
    {
        public static List<Hotel> GetHotels()
        {
            var hotels = 
                new List<Hotel>
                {
                    new Hotel("America", "NYC", "Hilton"),
                    new Hotel("America", "NYC", "Plaza"),
                    new Hotel("America", "NYC", "Pierre"),
                    new Hotel("America", "Vegas", "Caesars"),
                    new Hotel("America", "Vegas", "Mandalay"),
                    new Hotel("America", "Vegas", "Belagio"),
                    new Hotel("Australia", "Perth", "Burswood"),
                    new Hotel("Australia", "Perth", "Hyatt"),
                    new Hotel("Australia", "Perth", "Hilton"),
                    new Hotel("Australia", "Perth", "Duxton"),
                    new Hotel("Australia", "Perth", "Richardson")
                };

            return hotels;
        }

        private static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("System"))
            {
                var top = system.ActorOf(Props.Create<CategoryActor>(string.Empty, GetHotels(), true),
                    "TopLevelCategory");

                Thread.Sleep(2300);

                var f = top.Ask<Results>(new Query("America", "Vegas"));
                f.ContinueWith(x => { x.Result.Hotels.ToList().ForEach(Console.WriteLine); });

                Console.WriteLine("Press any ket to continue");
                Console.ReadKey();

                CoordinatedShutdown.Get(system).Run().Wait(TimeSpan.FromSeconds(5));
            }
        }
    }
}