// 1288 Remove Covered Intervals
// https://leetcode.com/problems/remove-covered-intervals/description/
// Difficulty: Medium
// Time Taken: 00:07:59

public class Solution {
  public int RemoveCoveredIntervals(int[][] intervals) {
    var ranges = intervals.OrderBy(x => x[0]).ThenByDescending(x => x[1] - x[0]).ToList();


    int last = 0;
    int remaining = intervals.Length;
    for (int idx = 1; idx < intervals.Length; idx++) {
      var lastRange = ranges[last];
      var (lx1, lx2) = (ranges[last][0], ranges[last][1]);
      var (cx1, cx2) = (ranges[idx][0], ranges[idx][1]);
      if (cx1 >= lx1 && cx2 <= lx2) {
        remaining -= 1;
      } else {
        last = idx;
      }
    }

    return remaining;
  }
}

public class MainClass {

  public record TC(int[][] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(Parse2D("[[1,4],[3,6],[2,8]]")),
      new TC(Parse2D("[[1,4],[2,3]]")),
    ];
    foreach (var tc in testcases) {
      var result = solution.RemoveCoveredIntervals(tc.a);
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

