using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;

namespace MaxFlow.Tests
{
    [TestClass]
    public class RouteFinderTests
    {
        private RouteFinder _finder;

        [TestInitialize]
        public void TestInitialize()
        {
            _finder = new RouteFinder();
        }

        [TestMethod]
        public void CanFinRoute()
        {
            var nodes = new List<Node>()
            {
                new Node() { Arcs = new List<Arc>() },
                new Node() { Arcs = new List<Arc>() },
                new Node() { Arcs = new List<Arc>() },
                new Node() { Arcs = new List<Arc>() }
            };

            nodes[0].Arcs = new List<Arc>()
            {
                new Arc() { Source = nodes[0], Destination = nodes[1], Capacity = 1 },
                new Arc() { Source = nodes[0], Destination = nodes[2], Capacity = 1 }
            };
            nodes[2].Arcs = new List<Arc>()
            {
                new Arc() { Source = nodes[2], Destination = nodes[3], Capacity = 1 },
            };

            var expected = new List<Arc>()
            {
                nodes[0].Arcs[1],
                nodes[2].Arcs[0]
            };

            var actual = _finder.FindRoute(nodes[0], nodes[3]);

            actual.arcs.Should().BeEquivalentTo(expected);
        }
    }
}
