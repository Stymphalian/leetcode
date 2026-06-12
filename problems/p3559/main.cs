// 3559 Number of Ways to Assign Edge Weights II
// https://leetcode.com/problems/number-of-ways-to-assign-edge-weights-ii/description/
// Difficulty: Hard
// Time Taken: 01:07:56

// LCA... yipiee. how can I remember this shit?


public class Node {
  public int depth;
  public int parent;
  public int enter;
  public int exit;
  public int[] ancestors;

  public Node(int depth, int parent, int enter, int limit) {
    this.depth = depth;
    this.parent = parent;
    this.enter = enter;
    exit = 0;
    ancestors = new int[limit+1];
  }
}

public class Solution {
  public const int MOD = 1000000007;

  public int DFS(int root,
    Dictionary<int, List<int>> adj,
    int from,
    int depth,
    ref int time,
    Dictionary<int, Node> record,
    int limit) {

    record[root] = new Node(depth, from, time, limit);
    time += 1;

    record[root].ancestors[0] = from;
    for(int idx  = 1; idx <= limit; idx++) {
      int parent = record[root].ancestors[idx-1];
      int parentParents = record[parent].ancestors[idx-1];
      record[root].ancestors[idx] = parentParents;
    }

    int best = 0;
    foreach (int child in adj[root]) {
      if (child == from) { continue; }
      int height = DFS(child, adj, root, depth + 1, ref time, record, limit);
      best = Math.Max(best, height + 1);
    }

    record[root].exit = time;
    time += 1;
    return best;
  }

  public long ModPower(int b, int exp, int mod) {
    if (exp <= 0) { return 1; }
    if (exp == 1) { return b; }
    if (exp % 2 == 0) {
      long cand = ModPower(b, exp / 2, mod);
      cand = (cand * cand) % mod;
      return cand;
    } else {
      long cand = ModPower(b, (exp - 1) / 2, mod);
      cand = (cand * cand) % mod;
      cand = (cand * b) % mod;
      return cand;
    }
  }

  public bool IsAncestor(int parent, int child, Dictionary<int, Node> nodes) {
    int enter = nodes[parent].enter;
    int exit = nodes[parent].exit;
    int b_enter = nodes[child].enter;
    int b_exit = nodes[child].exit;
    return enter <= b_enter && exit >= b_exit;
  }

  public int LowestCommonAncestor(int a, int b, Dictionary<int, Node> nodes, int limit) {
    if (IsAncestor(a, b, nodes)) { return a; }
    if (IsAncestor(b, a, nodes)) { return b; }
    for(int idx = limit; idx >= 0; idx--) {
      int parent = nodes[a].ancestors[idx];
      if (!IsAncestor(parent, b, nodes)) {
        a = parent;
      }
    }
    return nodes[a].ancestors[0];
  }

  public int[] AssignEdgeWeights(int[][] edges, int[][] queries) {
    Dictionary<int, List<int>> adj = [];
    foreach (var edge in edges) {
      var (from, to) = (edge[0], edge[1]);
      if (!adj.ContainsKey(from)) { adj.Add(from, []); }
      if (!adj.ContainsKey(to)) { adj.Add(to, []); }
      adj[from].Add(to);
      adj[to].Add(from);
    }
    Dictionary<int, Node> nodes = [];
    int time = 0;
    int n = adj.Count;
    int limit = (int)Math.Ceiling(Math.Log2(n));
    DFS(1, adj, 1, 0, ref time, nodes, limit);  // O(n log(n))

    List<int> answer = [];
    foreach (var query in queries) {
      var (from, to) = (query[0], query[1]);
      if (from == to) {
        answer.Add(0);
        continue;
      }

      int ancestor = LowestCommonAncestor(from, to, nodes, limit);
      int ancestorDepth = nodes[ancestor].depth;
      int fromDepth = nodes[from].depth;
      int toDepth = nodes[to].depth;

      int pathLen = 0;
      if (ancestor == from || ancestor == to) {
        // one is an ancestor of another, just take the difference in depth
        // to get the path len
        pathLen = Math.Abs(fromDepth - toDepth);
      } else {
        int left = fromDepth - ancestorDepth;
        int right = toDepth - ancestorDepth;
        pathLen = left + right;
      }
      int cand = (int)ModPower(2, pathLen - 1, MOD);
      answer.Add(cand);
    }

    return answer.ToArray();
  }
}

public class MainClass {

  public record TC(int[][] a, int[][] b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      // new TC(Parse2D("[[1,2]"), Parse2D("[[1,1],[1,2]]")),
      new TC(Parse2D("[[1,2],[1,3],[3,4],[3,5]]"), Parse2D("[[1,4],[3,4],[2,5]]")),
    ];
    foreach (var tc in testcases) {
      var result = solution.AssignEdgeWeights(tc.a, tc.b);
      PrintArray(result);
      // Console.WriteLine(result);
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

