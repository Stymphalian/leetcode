// 1846 Maximum Element After Decreasing and Rearranging
// https://leetcode.com/problems/maximum-element-after-decreasing-and-rearranging/description/
// Difficulty: Medium
// Time Taken: 00:09:47

public class Solution2 {
  public int MaximumElementAfterDecrementingAndRearranging(int[] arr) {
    int n = arr.Length;
    int[] freqs = new int[n+1];
    for(int idx = 0; idx < n; idx++) {
      int current = Math.Min(arr[idx], n);
      freqs[current] += 1;
    }

    int missing = 0;
    int cutoff = 0;
    for(int idx = 1; idx <= n; idx++) {
      if (freqs[idx] == 0) {
        missing += 1;
      } else if (freqs[idx] > 1) {
        if (freqs[idx] > missing) {
          int d = freqs[idx] - missing - 1;
          cutoff += d;
          missing = 0;
        } else {
          missing -= freqs[idx];
        }
      }
    }

    int highest = n - cutoff;
    return highest;
  }
}

public class Solution {
  public int MaximumElementAfterDecrementingAndRearranging(int[] arr) {
    arr.Sort();
    if (arr[0] != 1) {
      arr[0] = 1;
    }
    int best = arr[0];
    for (int idx = 1; idx < arr.Length; idx++) {
      int diff = Math.Abs(arr[idx-1] - arr[idx]);
      if (diff > 1) {
        arr[idx] = arr[idx-1] + 1;
      }
      best = Math.Max(arr[idx-1], arr[idx]);
    }
    return best;
  }
}

public class MainClass {

  public record TC(int[] a);


  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([2,2,1,2,1]),
      new TC([100,1,1000]),
      new TC([1,2,3,4,5]),
      new TC([73,98,9]),
    ];
    foreach (var tc in testcases) {
      var result = solution.MaximumElementAfterDecrementingAndRearranging(tc.a);
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

