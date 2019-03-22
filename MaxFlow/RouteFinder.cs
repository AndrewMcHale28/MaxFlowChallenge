using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxFlow
{
    public class RouteFinder
    {
        public (IEnumerable<Arc> arcs, IEnumerable<Node> reachableNodes) FindRoute(Node startNode, Node targetNode)
        {
            var reachedNodes = new Dictionary<Node, Arc>() { { startNode, null } };

            Arc nextArc = null;
            while (true)
            {
                nextArc = reachedNodes.Keys.SelectMany(n => n.Arcs)
                                .FirstOrDefault(a => a.Capacity > 0 && !reachedNodes.ContainsKey(a.Destination));

                if (nextArc != null)
                {
                    reachedNodes.Add(nextArc.Destination, nextArc);
 
                    if (nextArc.Destination == targetNode)
                    {
                        break;
                    }
                }
                else
                {
                    return (null, reachedNodes.Keys);
                }
            }

            var result = new List<Arc>();
            var backtrackArc = nextArc;
            while (backtrackArc != null)
            {
                result.Add(backtrackArc);
                backtrackArc = reachedNodes[backtrackArc.Source];
            }

            return (result, reachedNodes.Keys);
        }
    }
}
