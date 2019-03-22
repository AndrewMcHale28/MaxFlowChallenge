using System;
using System.Collections.Generic;
using System.Text;

namespace MaxFlow
{
    public class Node
    {
        public int Id { get; set; }

        public List<Arc> Arcs { get; set; }
    }
}
