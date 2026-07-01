// 2812 Find the Safest Path in a Grid
// https://leetcode.com/problems/find-the-safest-path-in-a-grid/description/
// Difficulty: Medium
// Time Taken: 02:20:52

// fuck.

public class Solution {

  public int ShortestPath(int[][] safety) {
    int n = safety.Length;
    PriorityQueue<(int, int, int), int> pq = new();
    HashSet<(int, int)> visited = [];

    visited.Add((0, 0));
    pq.Enqueue((0, 0, safety[0][0]), safety[0][0]);

    while (pq.Count > 0) {
      var (x, y, current) = pq.Peek();
      pq.Dequeue();

      if (x == n - 1 && y == n - 1) {
        return current;
      }

      foreach (var (dx, dy) in new[] { (1, 0), (0, 1), (-1, 0), (0, -1) }) {
        int nx = x + dx;
        int ny = y + dy;
        if (nx < 0 || nx >= n || ny < 0 || ny >= n) { continue; }
        if (visited.Contains((nx, ny))) { continue; }
        visited.Add((nx, ny));
        int priority = Math.Min(current, safety[ny][nx]);
        pq.Enqueue((nx, ny, priority), -priority);
      }
    }

    return 0;
  }

  public int[][] CalculateSafety(IList<IList<int>> grid) {
    int n = grid.Count;
    List<(int, int)> q = [];
    HashSet<(int, int)> seen = [];

    int[][] safety = new int[n][];
    for (int row = 0; row < n; row++) {
      safety[row] = new int[n];
      for (int col = 0; col < n; col++) {
        if (grid[row][col] == 1) {
          safety[row][col] = 0;
          seen.Add((col, row));
          q.Add((col, row));
        } else {
          safety[row][col] = -1;
        }
      }
    }

    
    while (q.Count > 0) {
      int size = q.Count;

      for (int idx = 0; idx < size; idx++) {
        var (x, y) = q[idx];
        int safe = safety[y][x];

        foreach (var (dx, dy) in new[] { (1, 0), (0, 1), (-1, 0), (0, -1) }) {
          int nx = x + dx;
          int ny = y + dy;
          if (nx < 0 || nx >= n || ny < 0 || ny >= n) { continue; }
          if (seen.Contains((nx, ny))) { continue; }

          safety[ny][nx] = safe + 1;
          seen.Add((nx, ny));
          q.Add((nx, ny));
        }
      }

      q.RemoveRange(0, size);
    }

    return safety;
  }

  public int MaximumSafenessFactor(IList<IList<int>> grid) {
    int[][] safety = CalculateSafety(grid);
    int answer = ShortestPath(safety);
    return answer;
  }
}


public class MainClass {

  public record TC(int[][] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(Parse2D("[[1,0,0],[0,0,0],[0,0,1]]")), // 0
      new TC(Parse2D("[[0,0,1],[0,0,0],[0,0,0]]")), // 2
      new TC(Parse2D("[[0,0,0,1],[0,0,0,0],[0,0,0,0],[1,0,0,0]]")), // 2
      new TC(Parse2D("[[0,1,1],[0,1,1],[0,1,1]]")), // 0
      new TC(Parse2D("[[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1],[0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0],[0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0],[0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,1,0,0],[0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,1,0,1,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0],[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0],[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0],[0,0,1,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,0,0,0],[1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0],[0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0]]")),

    ];
    foreach (var tc in testcases) {
      var result = solution.MaximumSafenessFactor(tc.a);
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

