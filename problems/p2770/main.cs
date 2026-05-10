// 2770 Maximum Number of Jumps to Reach the Last Index
// https://leetcode.com/problems/maximum-number-of-jumps-to-reach-the-last-index/description/
// Difficulty: Medium
// Time Taken: 00:23:03


public class Solution {

  bool CanJump(int[] nums, int target, int start, int end) {
    long diff = nums[end] - nums[start];
    return end > start && -target <= diff && diff <= target;
  }

  public int DP(int[] nums, int target, int current, Dictionary<int, int> memo) {
    int n = nums.Length;
    if (current == n-1) {
      return 0;
    }
    if (memo.ContainsKey(current)) {
      return memo[current];
    }

    // continue until we can find the left-most jump?
    int best = -1;
    for(int next = current + 1; next < n; next++) {
      if (CanJump(nums, target, current, next)) {
        int cand = DP(nums, target, next, memo);
        if (cand == -1) {continue;}
        best = Math.Max(best, cand + 1);
      }
    }
    memo[current] = best;
    return best;
  }

  public int MaximumJumps(int[] nums, int target) {
    return DP(nums, target, 0, []);  
  }
}

public class MainClass {

  public record TC(int[] a, int b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([1,3,6,4,1,2], 2),
      new TC([1,3,6,4,1,2], 3),
      new TC([1,3,6,4,1,2], 0),
      new TC([1,0,2], 1),
      new TC([-533985778,-424626669,794071124,694501105,-651162637,-789411200,773124493,-655591953,205086705,-421668572], 1194793065),
    ];
    foreach (var tc in testcases) {
      var result = solution.MaximumJumps(tc.a, tc.b);
      // PrintArray2D(result);
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

