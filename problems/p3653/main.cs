// 3653 XOR After Range Multiplication Queries I
// https://leetcode.com/problems/xor-after-range-multiplication-queries-i/description/
// Difficulty: Medium
// Time Taken: 00:11:35

public class Solution {
  public const long MOD = 1000000000 + 7;
  public int XorAfterQueries(int[] nums, int[][] queries) {

    foreach(var query in queries) {
      var (li, ri, ki, vi) = (query[0], query[1], query[2], query[3]);
      int idx = li;
      while (idx <= ri) {
        long n = (long) nums[idx];
        long result = n * vi;
        nums[idx] = (int) (result % MOD);
        idx += ki;
      }
    }

    long bitwise = 0;
    foreach(var n in nums) {
      bitwise ^= n;
    }
    return (int) bitwise;
  }
}

public class MainClass {

  public record TestCase(int[] e, int[][] c);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TestCase> testcases = [
      // new TestCase([1,1,1], Parse2D("[[0,2,1,4]]")),
      // new TestCase([2,3,1,5,4], Parse2D("[[1,4,2,3],[0,2,1,2]]")),
      new TestCase([780], Parse2D("[[0,0,1,13],[0,0,1,17],[0,0,1,9],[0,0,1,18],[0,0,1,16],[0,0,1,6],[0,0,1,4],[0,0,1,11],[0,0,1,7],[0,0,1,18],[0,0,1,8],[0,0,1,15],[0,0,1,12]]")),
    ];
    foreach(var tc in testcases) {
      var result = solution.XorAfterQueries(tc.e, tc.c); 
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

