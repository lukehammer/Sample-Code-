using FluentAssertions.Equivalency;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CodeWars
{


    public class Path
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int TravelTime { get; set; }

        public Path(int[] path)
        {
            Start = path[0];
            End = path[1];
            TravelTime = path[2];
        }

        public Path(int start, int end, int travelTime)
        {
            Start = start;
            End = end;
            TravelTime = travelTime;
        }
    }
    public class Network
    {

        public static int NetworkDelayTime(int[][] paths, int start, int end)
        {
            List<Path> network = paths.Select(path => new Path(path)).ToList();
            var validStartPaths = network.Where(x => x.Start == start).ToList();
            if (validStartPaths.Count == 0) return -1;
            IEnumerable<IEnumerable<int>> validPaths = new List<List<int>>();
            try
            {
                validPaths = GetValidPaths(network,start,end);
            }
            catch { return -1; }


            return -1;
        }

        public static int[][] CalculatePath(List<Path> network, IEnumerable<IEnumerable<int>> validPaths)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<IEnumerable<int>> GetValidPaths(List<Path> network, int start, int end)
        {
            var startPaths = network.Where(x =>x.Start == start).ToList();
            
            
            
            
            var validPaths = new List<IEnumerable<int>>();

            return validPaths;
        }
    }






    //public class Network
    //{
    //    public static HashSet<int> GetNodes(List<Path> paths)
    //    {

    //        var nodes = new HashSet<int>();
    //        foreach (var path in paths)
    //        {
    //            nodes.Add(path.Start);
    //            nodes.Add(path.End);
    //        }
    //        return nodes;
    //    }

    //    public static int NetworkDelayTime(int[][] map, int start, int target)
    //    {
    //        List<Path> paths = map.Select(x => new Path(x)).ToList();
    //        HashSet<int> nodes = GetNodes(paths);

    //        var nodeTimesFromStart = nodes.Select(x => new Node { Id = x }).ToList();

    //        foreach (var nodeTime in nodeTimesFromStart)
    //        {
    //            if (nodeTime.Id == start)
    //            { nodeTime.TimeFromStart = 0;}
    //        }

    //        var loops = 0;
    //        while (loops < 1000)
    //        {
    //            var timeToLocation = nodeTimesFromStart.SingleOrDefault(x => x.Id == start);

    //            if (timeToLocation?.TimeFromStart != null)
    //            { return timeToLocation.TimeFromStart.Value; }

                
    //            ClaculateNeigbors(nodeTimesFromStart, paths);
                



    //            loops++;
    //        }


    //        return -1;
    //    }

    //    public static void ClaculateNeigbors(List<Node> nodes, List<Path> paths)
    //    {
    //       var edgeNodes = nodes.Where(x => x.TimeFromStart != null && x.IsNextDoorMapped == false);

    //        List<Path> pathsFromCurrentEdge = new List<Path>();
    //        List<Path> pathsToCurrentEdge = new List<Path>();


    //        foreach (var edge in edgeNodes) 
    //        {
    //            pathsFromCurrentEdge = paths.Where(x => x.Start == edge.Id).ToList();
    //            pathsToCurrentEdge = paths.Where(x => x.End == edge.Id).ToList();
    //            pathsToCurrentEdge.ForEach(x => paths.Remove(x));
    //            pathsFromCurrentEdge.ForEach(x => paths.Remove(x));
    //            edge.IsNextDoorMapped = true;  
    //        }

    //        foreach (var pathFrom in pathsFromCurrentEdge)
    //        {
    //            var nodeTo = nodes.Where(x => x.Id == pathFrom.End).ToList();
    //            nodeTo.ForEach(x => x.TimeFromStart = pathFrom.TravelTime);
    //        }
    //        foreach (var pathTo in pathsToCurrentEdge)
    //        {
    //            var nodeFrom = nodes.Where(x => x.Id == pathTo.Start).ToList();
    //            nodeFrom.ForEach(x => x.TimeFromStart = pathTo.TravelTime);
    //        }

    //    }
    //}

}
