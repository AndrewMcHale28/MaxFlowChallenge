using System;
using System.Collections.Generic;
using System.Text;

namespace MaxFlow
{
    public class Arc
    {
        public Node Source { get; set; }
        public Node Destination { get; set; }
        public int InitialCapacity { get; set; }
        public int Capacity { get; set; }

        public Arc ReverseArc { get; set; }
    }
}
