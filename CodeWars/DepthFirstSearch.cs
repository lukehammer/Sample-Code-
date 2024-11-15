using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class DepthFirstSearch
    {
        static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
        static string startNode = "B";
        static string endNode = "F";
        static List<string> path = new List<string>();
        static List<string> shortestPath = new();

        static void Start()
        {
            // Build the graph
            graph["A"] = new List<string> { "B", "C" };
            graph["B"] = new List<string> { "A", "D", "E" };
            graph["C"] = new List<string> { "A", "F" };
            graph["D"] = new List<string> { "B" };
            graph["E"] = new List<string> { "B", "F" };
            graph["F"] = new List<string> { "C", "E" };

            // DFS to find the path
            DFS(startNode);

            if (shortestPath != null)
            {
                Console.WriteLine("Shortest Path: " + string.Join(" -> ", shortestPath));
            }
            else
            {
                Console.WriteLine("No path exists between the start and end nodes.");
            }

        }

        static void DFS(string currentNode)
        {
            path.Add(currentNode);

            if (currentNode == endNode && (shortestPath == null || path.Count < shortestPath.Count))
            {
                shortestPath = new List<string>(path);
            }

            if (!graph.ContainsKey(currentNode))
            {
                path.RemoveAt(path.Count - 1);
                return;
            }

            foreach (var neighbor in graph[currentNode])
            {
                if (path.Contains(neighbor)) continue;

                DFS(neighbor);
            }

            path.RemoveAt(path.Count - 1);
        }


        [Fact]
        public void Run() {
            Start();
        }
    }
}
