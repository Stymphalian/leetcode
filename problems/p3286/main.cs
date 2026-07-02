// 3286 Find a Safe Walk Through a Grid
// https://leetcode.com/problems/find-a-safe-walk-through-a-grid/description/
// Difficulty: Medium
// Time Taken: 00:52:16


public class Solution {
  public bool FindSafeWalk(IList<IList<int>> grid, int health) {
    int rows = grid.Count;
    int cols = grid[0].Count;
    HashSet<(int, int)> seen = [];
    PriorityQueue<(int, int), int> pq = new();
    pq.Enqueue((0, 0), -(health - grid[0][0]));

    while (pq.Count > 0) {
      if (pq.TryPeek(out (int, int) pos, out int currentHealth)) {
        pq.Dequeue();
        currentHealth *= -1;
        var (x, y) = pos;
        if (x == cols - 1 && y == rows - 1) {
          if (currentHealth > 0) {
            return true;
          }
        }

        foreach (var (dx, dy) in new[] { (1, 0), (0, 1), (-1, 0), (0, -1) }) {
          int nx = x + dx;
          int ny = y + dy;
          if (nx < 0 || nx >= cols || ny < 0 || ny >= rows) { continue; }
          if (seen.Contains((nx, ny))) { continue; }

          seen.Add((nx, ny));
          pq.Enqueue((nx, ny), -(currentHealth - grid[ny][nx]));
        }
      }
    }

    return false;
  }
}

public class MainClass {

  public record TC(int[][] a, int b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(Parse2D("[[0,1,0,0,0],[0,1,0,1,0],[0,0,0,1,0]]"), 1),
      new TC(Parse2D("[[0,1,1,0,0,0],[1,0,1,0,0,0],[0,1,1,1,0,1],[0,0,1,0,1,0]]"), 3),
      new TC(Parse2D("[[1,1,1],[1,0,1],[1,1,1]]"), 5),
    ];
    foreach (var tc in testcases) {
      var result = solution.FindSafeWalk(tc.a, tc.b);
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

