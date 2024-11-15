using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class Graph
    {
        private List<(int, int)> connectedPoints;

        public Dictionary<int, HashSet<int>> AdjacencyList { get; }

        public Graph(List<(int, int)> connectedPoints)
        {
            this.connectedPoints = connectedPoints;
            AdjacencyList = CalculateAdjacencyList();
        }

        private Dictionary<int, HashSet<int>> CalculateAdjacencyList()
        {
            var adjacency = new Dictionary<int, HashSet<int>>();
            foreach (var edge in connectedPoints) 
            {
                if (!adjacency.ContainsKey(edge.Item1))
                {
                    adjacency.TryAdd(edge.Item1, new HashSet<int>() );
                }
                adjacency[edge.Item1].Add(edge.Item2);
        
            }
            return adjacency;
        }

        internal bool ArePointsConnected(int origin, int destination)
        {
            var search = new Stack<int>();
            search.Push(origin);
            while (search.Count > 0)
            {
                var current = search.Pop();
                if (AdjacencyList.TryGetValue(current, out var listForCurrent))
                foreach (var neighbor in listForCurrent)
                {
                    if (neighbor.Equals(destination)) return true;
                    search.Push(neighbor);
                }
            }
            return false;
        }
    }

    public class GraphTests 
    {
        [Fact]
        public void Two_and_one_connected()
        {
            List<(int,int)>  connectedPoints = new List<(int, int)> { (1, 2), (2, 3), (100,101) };
            var graph = new Graph(connectedPoints);
            graph.ArePointsConnected(1,2).Should().BeTrue();
        
        }

        [Fact]
        public void one_and_100_are_not_connected()
        {
            List<(int, int)> connectedPoints = new List<(int, int)> { (1, 2), (2, 3), (100, 101) };
            var graph = new Graph(connectedPoints);
            graph.ArePointsConnected(1, 100).Should().BeFalse();
        }


        [Fact]
        public void one_and_5_are_not_connected()
        {
            List<(int, int)> connectedPoints = new List<(int, int)> { (1, 2), (2, 3), (3, 4), (4, 5), (2, 3), (100, 101) };
            var graph = new Graph(connectedPoints);
            graph.ArePointsConnected(1, 5).Should().BeTrue();

        }


    }
}
