using System.Collections.Generic;
using Akka.Actor;
using Akka.ScatterGather.CmdLine.Domain;

namespace Akka.ScatterGather.CmdLine.Actors
{
    public class AggregatorActor : ReceiveActor
    {
        private List<Hotel> _results;
        private int _seen;

        public AggregatorActor(IActorRef original, int waitFor)
        {
            _results = new List<Hotel>();
            Receive<Results>(x =>
            {
                if (++_seen == waitFor)
                {
                    original.Tell(x);
                    Self.Tell(PoisonPill.Instance);
                }
            });
        }
    }
}