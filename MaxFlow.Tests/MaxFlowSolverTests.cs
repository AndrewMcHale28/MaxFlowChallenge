using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using System.Linq;

namespace MaxFlow.Tests
{
    [TestClass]
    public class MaxFlowSolverTests
    {
        private MaxFlowSolver _flowSolver;

        [TestInitialize]
        public void TestInitialize()
        {
            _flowSolver = new MaxFlowSolver();
        }

        [TestMethod]
        public void SolvesASimpleProble()
        {
            var input = "[ { from: 0, to: 1, capacity: 10 },"
                       + " { from: 1, to: 2, capacity: 7 },"
                       + " { from: 1, to: 3, capacity: 30 },"
                       + " { from: 3, to: 1, capacity: 8 } ]";

            var actual = _flowSolver.FindMaxFlow(input);

            actual.maxFlow.Should().Be(10);
            var limitingArcs = actual.limitingArcs.ToList();
            limitingArcs.Count().Should().Be(1);
            limitingArcs.Any(a => a.Source.Id == 0 && a.Destination.Id == 1).Should().Be(true);
        }

        [TestMethod]
        public void SolvesWithTwoPossibleRoutes()
        {
            var input = "[ { from: 0, to: 1, capacity: 30 },"
                       + " { from: 1, to: 2, capacity: 6 },"
                       + " { from: 1, to: 3, capacity: 15 },"
                       + " { from: 2, to: 3, capacity: 9 } ]";

            var actual = _flowSolver.FindMaxFlow(input);

            actual.maxFlow.Should().Be(21);
            var limitingArcs = actual.limitingArcs.ToList();
            limitingArcs.Count().Should().Be(2);
            limitingArcs.Any(a => a.Source.Id == 1 && a.Destination.Id == 2).Should().Be(true);
            limitingArcs.Any(a => a.Source.Id == 1 && a.Destination.Id == 3).Should().Be(true);
        }

        [TestMethod]
        public void SolvesSetProblem()
        {
            var input = "[ { from: 0, to: 1, capacity: 123 },"
                      + "  { from: 0, to: 2, capacity: 32 },"
                      + "  { from: 1, to: 2, capacity: 12 },"
                      + "  { from: 1, to: 4, capacity: 45 },"
                      + "  { from: 2, to: 3, capacity: 3 },"
                      + "  { from: 3, to: 6, capacity: 27 },"
                      + "  { from: 3, to: 9, capacity: 2 },"
                      + "  { from: 4, to: 5, capacity: 30 },"
                      + "  { from: 4, to: 7, capacity: 5 },"
                      + "  { from: 5, to: 6, capacity: 97 },"
                      + "  { from: 5, to: 8, capacity: 54 },"
                      + "  { from: 6, to: 9, capacity: 15 },"
                      + "  { from: 7, to: 8, capacity: 10 },"
                      + "  { from: 8, to: 9, capacity: 9 },"
                      + "  { from: 5, to: 7, capacity: 36 },"
                      + "  { from: 5, to: 1, capacity: 57 },"
                      + "  { from: 8, to: 5, capacity: 3 } ]";

            var actual = _flowSolver.FindMaxFlow(input);

            actual.maxFlow.Should().Be(26);
            var limitingArcs = actual.limitingArcs.ToList();
            limitingArcs.Count().Should().Be(3);
            limitingArcs.Any(a => a.Source.Id == 3 && a.Destination.Id == 9).Should().Be(true);
            limitingArcs.Any(a => a.Source.Id == 6 && a.Destination.Id == 9).Should().Be(true);
            limitingArcs.Any(a => a.Source.Id == 8 && a.Destination.Id == 9).Should().Be(true);
        }

        [TestMethod]
        public void SolvesWithZeroRemainingCapacityNodesNotPartOfTheCut()
        {
            var input = "[ { from: 0, to: 1, capacity: 2 },"
                       + " { from: 0, to: 2, capacity: 2 },"
                       + " { from: 1, to: 3, capacity: 20 },"
                       + " { from: 2, to: 3, capacity: 20 },"
                       + " { from: 3, to: 4, capacity: 2 } ]";

            var actual = _flowSolver.FindMaxFlow(input);

            actual.maxFlow.Should().Be(2);
            var limitingArcs = actual.limitingArcs.ToList();
            limitingArcs.Count().Should().Be(1);
            limitingArcs.Any(a => a.Source.Id == 3 && a.Destination.Id == 4).Should().Be(true);
        }
    }
}
