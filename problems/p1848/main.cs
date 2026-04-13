// 1848 Minimum Distance to the Target Element
// https://leetcode.com/problems/minimum-distance-to-the-target-element/description/
// Difficulty: Easy
// Time Taken: 00:04:57


public class Solution {
  public int GetMinDistance(int[] nums, int target, int start) {
    int best = int.MaxValue;
    for(int index = 0; index  < nums.Length; index++) {
      if (nums[index] == target) {
        best = Math.Min(best, Math.Abs(index - start));
      }
    }

    return best;
  }
}


public class MainClass {

  public record TC(int[] e, int t, int s);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([1,2,3,4,5], 5, 3),
      new TC([1], 1, 0),
      new TC([1,1,1,1,1,1,1,1,1,1], 1, 0),
    ];
    foreach (var tc in testcases) {
      var result = solution.GetMinDistance(tc.e, tc.t, tc.s);
      Console.WriteLine(result);
    }
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

