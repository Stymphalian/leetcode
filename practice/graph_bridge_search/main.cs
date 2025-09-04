// https://www.spoj.com/problems/GRAFFDEF/
// https://cp-algorithms.com/graph/bridge-searching.html

using System;
using System.Collections.Generic;

public class Test {
    public class Solution {

        bool[] _visited;
        int[] _timeIn;
        int[] _lowestTime;
        int _timer = 0;
        HashSet<(int,int)> _bridges;

        public void FindBridges(int current, int parent, Dictionary<int, List<int>> adjList) {
            _visited[current] = true;
            _timeIn[current] = _timer;
            _lowestTime[current] = _timer;
            _timer += 1;


            foreach (var to in adjList[current]) {
                if (to == parent) { continue; }

                if (_visited[to]) {
                    // A possible back-edge
                    _lowestTime[current] = Math.Min(_timeIn[to], _lowestTime[current]);
                } else {
                    // forward edge
                    FindBridges(to, current, adjList);

                    _lowestTime[current] = Math.Min(_lowestTime[current], _lowestTime[to]);
                    if (_lowestTime[to] > _timeIn[current]) {
                        // bridge edge
                        _bridges.Add((current, to));
                        _bridges.Add((to, current));
                    }
                }
            }
        }


        public long DFS(int node, Dictionary<int, List<int>> adjList) {
            _visited[node] = true;

            long count = 1;
            foreach (var to in adjList[node]) {
                if (_visited[to] || _bridges.Contains((node, to))) { continue; }
                count += DFS(to, adjList);
            }
            return count;
        }

        public long NChooseK(int n, int k) {
            long result = 1;
            for (int i = 0; i < k; i++) {
                result *= n - i;
                result /= i + 1;
            }
            return result;
        }

        public double Solve(int numNodes, List<(int, int)> edges) {
            _visited = new bool[numNodes + 1];
            _timeIn = new int[numNodes + 1];
            _lowestTime = new int[numNodes + 1];
            _timer = 0;
            _bridges = new HashSet<(int,int)>();
            Dictionary<int, List<int>> adjList = new Dictionary<int, List<int>>();
            for(int index = 0; index < edges.Count; index++) {
                int a = edges[index].Item1;
                int b = edges[index].Item2;
                if (!adjList.ContainsKey(a)) {
                    adjList[a] = new List<int>();
                }
                if (!adjList.ContainsKey(b)) {
                    adjList[b] = new List<int>();
                }
                adjList[a].Add(b);
                adjList[b].Add(a);
            }
            // Graph is fully connected to begin with so we can do a single FindBridges DFS pass
            FindBridges(1, -1, adjList);

            // With the list of bridges, run a DFS to find all the isolated sub-graphs
            // and get the size of each of these sub-graphs
            // Array.Clear(_visited);
            _visited = new bool[numNodes + 1];
            List<long> subGraphs = new List<long>();
            for (int node = 1; node <= numNodes; node++) {
                if (!_visited[node]) {
                    long count = DFS(node, adjList);
                    subGraphs.Add(count);
                }
            }

            long totalPositions = (long) numNodes * ((long) numNodes - 1) / 2;
            long successfulPositions = 0;
            foreach (var subGraph in subGraphs) {
                successfulPositions += subGraph * (subGraph - 1) / 2;
            }

            return 1.0 - (double)successfulPositions / totalPositions;
        }
    }


    public static void Main() {
        Solution s = new Solution();

        string? line;
        int numNodes = 0;
        int numEdges = 0;
        List<(int, int)> edges = new List<(int, int)>(); ;
        int state = 0;
        while ((line = Console.ReadLine()) != null) {
            var parts = line.Split(" ");
            var a = int.Parse(parts[0]);
            var b = int.Parse(parts[1]);
            if (state == 0) {
                numNodes = a;
                numEdges = b;
                state = 1;
            } else {
                edges.Add((a, b));
            }
        }

        // var numNodes = 4;
        // var edges = new List<(int, int)> { (1, 2), (1,3), (2,4), (4,1) };
        // var numNodes = 6;
        // var edges = new List<(int, int)> { (1, 2), (1,3), (2,4), (4,1), (5,3), (6,3), (5,6) };
        // var numNodes = 7;
        // var edges = new List<(int,int)> { (1,2), (2,3), (2,4), (3,4), (3,5), (5,6), (6,7), (5,7) }; 
        Console.WriteLine(string.Format("{0:0.00000}", s.Solve(numNodes, edges)));
    }
}
