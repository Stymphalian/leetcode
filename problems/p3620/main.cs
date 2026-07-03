// 3620 Network Recovery Pathways
// https://leetcode.com/problems/network-recovery-pathways/description/
// Difficulty: Hard
// Time Taken: 02:10:01

// nope


public class Solution {
  public bool Check(Dictionary<int, List<(int, int)>> graph, int n, long k, int minEdgeCost) {

    long[] dist = new long[n];
    Array.Fill(dist, long.MaxValue);
    dist[0] = 0;

    PriorityQueue<(int, int), int> pq = new();
    pq.Enqueue((0, 0), 0);

    while (pq.Count > 0) {
      var (node, currentCost) = pq.Peek();
      pq.Dequeue();

      if (currentCost > k) { return false; }
      if (node == n - 1) { return true; }
      if (currentCost > dist[node]) {
        continue;
      }

      foreach (var (child, edgeCost) in graph[node]) {
        int nextCost = currentCost + edgeCost;
        if (nextCost > k) { continue; }
        if (edgeCost < minEdgeCost) {continue;}
        if (dist[child] > nextCost) {
          dist[child] = nextCost;
          pq.Enqueue((child, nextCost), nextCost);    
        }
      }
    }

    return false;
  }

  public int FindMaxPathScore(int[][] edges, bool[] online, long k) {
    int n = online.Length;
    Dictionary<int, List<(int, int)>> graph = [];
    for (int idx = 0; idx < n; idx++) { graph[idx] = []; }

    int minCost = int.MaxValue;
    int maxCost = 0;
    foreach (var edge in edges) {
      var (from, to, cost) = (edge[0], edge[1], edge[2]);
      if (!online[from] || !online[to]) { continue; }
      if (!graph.ContainsKey(from)) {
        graph[from] = [];
      }
      graph[from].Add((to, cost));
      minCost = Math.Min(minCost, cost);
      maxCost = Math.Max(maxCost, cost);
    }


    // binary search
    int left = minCost;
    int right = maxCost;
    int answer = -1;
    while (left <= right) {
      int mid = left + (right - left) / 2;
      if (Check(graph, n, k, mid)) {
        answer = mid;
        left = mid + 1;
      } else {
        right = mid - 1;
      }
    }

    return answer;
  }
}

public class MainClass {

  public record TC(int[][] a, bool[] b, long c);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(Parse2D("[[0,1,5],[1,3,10],[0,2,3],[2,3,4]]"), [true, true, true, true], 10),
      new TC(Parse2D("[[0,1,7],[1,4,5],[0,2,6],[2,3,6],[3,4,2],[2,4,6]]"), [true, true, true, false, true], 12),
      new TC(Parse2D("[[0,2,90],[1,2,36],[0,1,61]]"), [true, true, true], 129),
      new TC(Parse2D("[[2,3,96],[1,3,32],[0,3,28],[1,2,37],[0,1,77],[0,2,27]]"), [true, true, false, true], 168),
      new TC(Parse2D("[[2,3,50],[3,4,65],[0,1,91],[1,4,47],[0,3,24],[1,3,53]]"), [true, true, true, true, true], 254),
    ];
    foreach (var tc in testcases) {
      var result = solution.FindMaxPathScore(tc.a, tc.b, tc.c);
      // PrintListNode(result);
      // PrintArray(result);
      Console.WriteLine(result);
    }
  }

  public static T[][] P2D<T>(T[,] arr) {
    int rows = arr.GetLength(0);
    int cols = arr.GetLength(1);
    T[][] output = new T[rows][];
    for (int row = 0; row < rows; row++) {
      output[row] = new T[cols];
      for (int col = 0; col < cols; col++) {
        output[row][col] = arr[row, col];
      }
    }
    return output;
  }

  public static int[] Parse1D(string input) {
    var array = Array.ConvertAll(input.Trim('[', ']').Split(','), int.Parse);
    return array;
  }

  // [["#",".","*","."],["#","#","*","."]]
  public static char[][] ParseChar2D(string input) {
    input = input.Trim();
    var inner = input.Substring(1, input.Length - 2).Trim();
    if (inner == "") return new char[0][];
    var rows = inner.Split("],[");
    var array = new char[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
      var row = rows[i].Trim('[', ']').Trim();
      array[i] = row == "" ? new char[0] : row.Split(',').Select(s => s.Trim('"', ' ')[0]).ToArray();
    }
    return array;
  }

  public static int[][] Parse2D(string input) {
    input = input.Trim();
    var inner = input.Substring(1, input.Length - 2).Trim();
    if (inner == "") return new int[0][];
    var rows = inner.Split("],[");
    var array = new int[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
      var row = rows[i].Trim('[', ']').Trim();
      array[i] = row == "" ? new int[0] : Array.ConvertAll(row.Split(','), s => int.Parse(s.Trim()));
    }
    return array;
  }

  public static void PrintArray<T>(IEnumerable<T> array, string prefix = "") {
    Console.WriteLine($"{prefix}[{string.Join(", ", array)}]");
  }

  public static void PrintArray2D<T>(T[][] array) {
    Console.WriteLine("[");
    foreach (var row in array) {
      PrintArray(row, " ");
    }
    Console.WriteLine("]");
  }
};

