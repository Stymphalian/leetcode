// 1840 Maximum Building Height
// https://leetcode.com/problems/maximum-building-height/description/
// Difficulty: Hard
// Time Taken: 02:57:57

// you are a failure.

public class Solution {
  public int MaxBuilding(int n, int[][] restrictions) {

    // Get all the limits in ascending index order
    List<int[]> limits = [];
    bool has_n = false;
    foreach (var res in restrictions) {
      var (building, height) = (res[0], res[1]);
      limits.Add([building, height]);
      if (building == n) { has_n = true; }
    }
    limits.Add([1, 0]);
    if (!has_n) { limits.Add([n, n - 1]); }
    limits = limits.OrderBy(x => x[0]).ToList();
    int m = limits.Count;

    // left to right pass
    for (int left = 0; left < m - 1; left++) {
      int right = left + 1;
      int dist = limits[right][0] - limits[left][0];

      int currentHeight = limits[right][1];
      int candidateHeight = limits[left][1] + dist;
      int bestHeight = Math.Min(currentHeight, candidateHeight);
      limits[right][1] = bestHeight;
    }

    // right to left pass
    for (int right = m - 1; right >= 1; right--) {
      int left = right - 1;
      int dist = limits[right][0] - limits[left][0];

      int currentHeight = limits[left][1];
      int candidateHeight = limits[right][1] + dist;
      int bestHeight = Math.Min(currentHeight, candidateHeight);
      limits[left][1] = bestHeight;
    }

    // get the maximum
    int best = 0;
    for (int left = 0; left < m - 1; left++) {
      int right = left + 1;
      int dist = limits[right][0] - limits[left][0];
      int cand = (dist + limits[left][1] + limits[right][1]) / 2;
      best = Math.Max(best, cand);
    }
    return best;
  }
}



public class MainClass {

  public record TC(int a, int[][] b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(5, Parse2D("[[2,1],[4,1]]")),
      new TC(6, Parse2D("[]")),
      new TC(10, Parse2D("[[5,3],[2,5],[7,4],[10,3]]")),
      new TC(10, Parse2D("[[6,2],[5,1],[2,4],[3,0],[9,5],[7,0],[4,2],[10,3],[8,0]]")),
    ];
    foreach (var tc in testcases) {
      var result = solution.MaxBuilding(tc.a, tc.b);
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

