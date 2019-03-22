using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using System.Linq;

namespace MaxFlow.Tests
{
    [TestClass]
    public class NetworkBuilderTests
    {
        private NetworkBuilder _builder;

        [TestInitialize]
        public void TestInitialize()
        {
            _builder = new NetworkBuilder();
        }

        [TestMethod]
        public void CanBuildNetwork()
        {
            var input = "[ {\"from\": 0, \"to\": 1, \"capacity\": 10 },"
                       + " {\"from\": 1, \"to\": 2, \"capacity\": 7 },"
                       + " {\"from\": 1, \"to\": 3, \"capacity\": 30 },"
                       + " {\"from\": 3, \"to\": 1, \"capacity\": 8 } ]";

            var actual = _builder.BuildNetwork(input);

            actual.Count.Should().Be(4);
            var zeroNode = actual.First(n => n.Id == 0);
            var oneNode = actual.First(n => n.Id == 1);
            var twoNode = actual.First(n => n.Id == 2);
            var threeNode = actual.First(n => n.Id == 3);

            zeroNode.Arcs.Count.Should().Be(1);
            zeroNode.Arcs[0].Destination.Should().Be(oneNode);
            zeroNode.Arcs[0].Capacity.Should().Be(10);
            zeroNode.Arcs[0].InitialCapacity.Should().Be(10);

            oneNode.Arcs.Count.Should().Be(2);
            oneNode.Arcs[0].Destination.Should().Be(twoNode);
            oneNode.Arcs[0].Capacity.Should().Be(7);
            oneNode.Arcs[0].InitialCapacity.Should().Be(7);
            oneNode.Arcs[1].Destination.Should().Be(threeNode);
            oneNode.Arcs[1].Capacity.Should().Be(30);
            oneNode.Arcs[0].InitialCapacity.Should().Be(7);

            twoNode.Arcs.Count.Should().Be(0);

            threeNode.Arcs.Count.Should().Be(1);
            threeNode.Arcs[0].Destination.Should().Be(oneNode);
            threeNode.Arcs[0].Capacity.Should().Be(8);
            threeNode.Arcs[0].Capacity.Should().Be(8);

            oneNode.Arcs[1].ReverseArc.Should().Be(threeNode.Arcs[0]);
            threeNode.Arcs[0].ReverseArc.Should().Be(oneNode.Arcs[1]);
        }
    }
}
