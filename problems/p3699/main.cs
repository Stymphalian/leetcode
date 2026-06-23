// 3699 Number of ZigZag Arrays I
// https://leetcode.com/problems/number-of-zigzag-arrays-i/description/
// Difficulty: Hard
// Time Taken: 03:05:09

// close but you still shit dumb

public class Solution {

  public const int UP = 0;
  public const int DOWN = 1;
  public const int MOD = 1_000_000_007;

  public int ZigZagArrays(int n, int l, int r) {
    int m = r - l;

    int[][] dp = new int[m+1][];
    int[][] sums = new int[m+1][];
    for(int idx = 0; idx <= m; idx++) {
      dp[idx] = new int[2]{1,1};
    }
    for(int idx = 0; idx <= m; idx++) {
      sums[idx] = new int[2];
      sums[idx][UP] = (idx-1 >= 0 ? sums[idx-1][UP] : 0) + 1;
      sums[idx][DOWN] = (idx-1 >= 0 ? sums[idx-1][DOWN] : 0) + 1;
    }

    for(int ni = 1; ni < n; ni++) {
      for(int col = 0; col <= m; col++) {
        dp[col][DOWN] = (col-1 >= 0 ? sums[col-1][UP] : 0);
        dp[col][UP] = (sums[m][DOWN] - sums[col][DOWN] + MOD) % MOD;
      }

      // Prefix sum
      sums[0][DOWN] = dp[0][DOWN];
      sums[0][UP] = dp[0][UP];
      for(int col = 1; col <= m; col++) {
        sums[col][DOWN] = (sums[col-1][DOWN] + dp[col][DOWN]) % MOD;
        sums[col][UP] = (sums[col-1][UP] + dp[col][UP]) % MOD;
      }
    }

    return (sums[m][DOWN] + sums[m][UP]) % MOD;
  }
}


public class MainClass {

  public record TC(int a, int b, int c);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(3, 4, 5), // 3
      new TC(3, 1, 3), // 10
      new TC(3, 1, 4), // 28
      new TC(3, 4, 10),
      new TC(3, 4, 9),
    ];
    foreach (var tc in testcases) {
      var result = solution.ZigZagArrays(tc.a, tc.b, tc.c);
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

