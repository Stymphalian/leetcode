// 2492 Minimum Score of a Path Between Two Cities
// https://leetcode.com/problems/minimum-score-of-a-path-between-two-cities/description/
// Difficulty: Medium
// Time Taken: 00:40:46

// Read that shit.

using System.Diagnostics;

public class Solution {
  public class UnionFind {

    int[] parents;
    int[] rank;
    public UnionFind(int n) {
      this.parents = new int[n];
      this.rank = new int[n];
      for (int idx = 0; idx < n; idx++) {
        parents[idx] = idx;
        rank[idx] = 0;
      }
    }

    public int Find(int a) {
      int current = a;
      while (current != parents[current]) {
        current = parents[current];
      }
      parents[a] = current;
      return current;
    }

    public void Union(int a, int b) {
      int parent_a = Find(a);
      int parent_b = Find(b);
      if (parent_a == parent_b) {
        return;
      }

      int rank_a = rank[parent_a];
      int rank_b = rank[parent_b];
      if (rank_a < rank_b) {
        parents[parent_a] = parent_b;
        rank[parent_b] += 1;
      } else {
        parents[parent_b] = parent_a;
        rank[parent_a] += 1;
      }
    }

  }

  public int MinScore(int n, int[][] roads) {
    UnionFind uf = new(n+1);
    foreach (var road in roads) {
      var (from, to) = (road[0], road[1]);
      uf.Union(from, to);
    }
    int root_end = uf.Find(n);
    int best = int.MaxValue;
    foreach (var road in roads) {
      var (from, to, distance) = (road[0], road[1], road[2]);
      int cand_parent = uf.Find(from);
      if (cand_parent == root_end) {
        best = Math.Min(distance, best);
      }
    }

    return best;
  }
}

public class MainClass {

  public record TC(int a, int[][] b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(4, Parse2D("[[1,2,9],[2,3,6],[2,4,5],[1,4,7]]")),
      new TC(4, Parse2D("[[1,2,2],[1,3,4],[3,4,7]]")),
    ];
    foreach (var tc in testcases) {
      var result = solution.MinScore(tc.a, tc.b);
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

