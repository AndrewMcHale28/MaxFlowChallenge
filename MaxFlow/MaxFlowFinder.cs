using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxFlow
{
    public class MaxFlowFinder
    {
        private RouteFinder _routeFinder;

        public MaxFlowFinder()
        {
            _routeFinder = new RouteFinder();
        }

        public (int maxFlow, IEnumerable<Arc> limitingArcs) FindMaxFlow(Node startNode, Node targetNode)
        {
            var totalFlow = 0;
            (IEnumerable<Arc> arcs, IEnumerable<Node> reachableNodes) newRoute;
            while ((newRoute = _routeFinder.FindRoute(startNode, targetNode)).arcs != null)
            {
                var maxRouteFlow = newRoute.arcs.Min(r => r.Capacity);

                foreach (var arc in newRoute.arcs)
                {
                    arc.Capacity -= maxRouteFlow;
                    
                    if (arc.ReverseArc == null)
                    {
                        arc.ReverseArc = new Arc()
                        {
                            ReverseArc = arc,
                            Destination = arc.Source,
                            Source = arc.Destination,
                            InitialCapacity = 0,
                            Capacity = 0
                        };
                        arc.Destination.Arcs.Add(arc.ReverseArc);
                    }

                    arc.ReverseArc.Capacity += maxRouteFlow;
                }

                totalFlow += maxRouteFlow;
            }

            var reachableNodesSet = newRoute.reachableNodes.ToHashSet();
            var blockingArcs = newRoute.reachableNodes.SelectMany(r => r.Arcs)
                .Where(a => a.InitialCapacity > 0 && !reachableNodesSet.Contains(a.Destination));
            return (totalFlow, blockingArcs);
        }
    }
}
