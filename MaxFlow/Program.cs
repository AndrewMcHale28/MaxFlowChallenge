using System;
using System.IO;

namespace MaxFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileJson = File.ReadAllText(@"..\..\..\..\Network.json");
            var solver = new MaxFlowSolver();
            var result = solver.FindMaxFlow(fileJson);

            Console.WriteLine($"Max flow: {result.maxFlow}");
            Console.WriteLine("Limiting arcs:");

            foreach (var arc in result.limitingArcs)
            {
                Console.WriteLine($"{arc.Source.Id} - {arc.Destination.Id}");
            }

            Console.ReadLine();
        }
    }
}
