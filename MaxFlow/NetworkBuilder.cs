using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxFlow
{
    public class NetworkBuilder
    {
        public List<Node> BuildNetwork(string jsonString)
        {
            var arcs = JsonConvert.DeserializeObject<IEnumerable<JSONArc>>(jsonString);

            var nodeCount = arcs.Max(a => a.to) + 1;
            var result = Enumerable.Range(0, nodeCount).Select(r => new Node() { Id = r, Arcs = new List<Arc>() }).ToList();
            var nodesMap = result.ToDictionary(n => n.Id);

            foreach (var arc in arcs)
            {
                var source = nodesMap[arc.from];
                var target = nodesMap[arc.to];

                var newArc = new Arc()
                {
                    Capacity = arc.capacity,
                    InitialCapacity = arc.capacity,
                    Destination = target,
                    Source = source
                };

                source.Arcs.Add(newArc);

                var returnArc = target.Arcs.FirstOrDefault(a => a.Destination == source);
                if (returnArc != null)
                {
                    newArc.ReverseArc = returnArc;
                    returnArc.ReverseArc = newArc;
                }
            }

            return result;
        }
    }
}
