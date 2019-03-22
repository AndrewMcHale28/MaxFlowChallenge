using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaxFlow
{
    public class MaxFlowSolver
    {
        private NetworkBuilder _networkBuilder;
        private MaxFlowFinder _flowFinder;

        public MaxFlowSolver()
        {
            _networkBuilder = new NetworkBuilder();
            _flowFinder = new MaxFlowFinder();
        }

        public (int maxFlow, IEnumerable<Arc> limitingArcs) FindMaxFlow(string networkJson)
        {
            var network = _networkBuilder.BuildNetwork(networkJson);

            return _flowFinder.FindMaxFlow(network.First(), network.Last());
        }
    }
}
