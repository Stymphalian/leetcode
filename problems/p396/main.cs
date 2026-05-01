// 396 Rotate Function
// https://leetcode.com/problems/rotate-function/description/
// Difficulty: Medium
// Time Taken: 00:25:13

public class Solution {
  public int MaxRotateFunction(int[] nums) {
    int sum = 0;
    int F = 0;
    for(int index = 0; index < nums.Length; index++) {
      sum += nums[index];
      F += index * nums[index];
    }

    int best = F;
    for(int k = 1; k <= nums.Length-1; k++) {
      int end = nums[(k + nums.Length-1) % nums.Length];
      F = F - sum + nums.Length * end;
      best = Math.Max(F, best);
    }
    return best;
  }
}

public class MainClass {

  public record TC(int[] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([4,3,2,6]),
      new TC([100])
    ];
    foreach (var tc in testcases) {
      var result = solution.MaxRotateFunction(tc.a);
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

