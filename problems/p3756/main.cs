// 3756 Concatenate Non-Zero Digits and Multiply by Sum II
// https://leetcode.com/problems/concatenate-non-zero-digits-and-multiply-by-sum-ii/description/
// Difficulty: Medium
// Time Taken: 01:02:18

// who knew that modulus works over digit concatenation.

public class Solution {
  // 10_000_000_000
  public const long MOD = 1_000_000_007;

  public long power10Mod(long degree, Dictionary<long, long> memo) {
    if (degree == 0) { return 1; }
    if (degree == 1) { return 10; }
    if (memo.ContainsKey(degree)) {
      return memo[degree];
    }
    if (degree % 2 == 0) {
      long even = power10Mod(degree / 2, memo);
      memo[degree] = (even * even) % MOD;
    } else {
      long even = power10Mod((degree - 1) / 2, memo);
      long result = ((even * even) % MOD);
      memo[degree] = (result * 10) % MOD;
    }
    return memo[degree];
  }

  public int[] SumAndMultiply(string s, int[][] queries) {
    int[] answer = new int[queries.Length];

    long[] mults = new long[s.Length + 1];
    long[] lengths = new long[s.Length + 1];
    long[] sums = new long[s.Length + 1];
    mults[0] = 0;
    lengths[0] = 0;
    sums[0] = 0;

    for (int idx = 1; idx <= s.Length; idx++) {
      int digit = s[idx - 1] - '0';
      mults[idx] = mults[idx - 1];
      lengths[idx] = lengths[idx - 1];
      sums[idx] = sums[idx - 1];
      if (digit != 0) {
        mults[idx] = (mults[idx - 1] * 10 + digit) % MOD;
        lengths[idx] = lengths[idx - 1] + 1;
        sums[idx] = (sums[idx - 1] + digit) % MOD;
      }
    }

    Dictionary<long, long> memo = [];
    for (int ai = 0; ai < queries.Length; ai++) {
      int[] query = queries[ai];
      int left = query[0] + 1;
      int right = query[1] + 1;

      long power = power10Mod(lengths[right] - lengths[left - 1], memo);
      long mult_left = (mults[left-1] * power) % MOD;
      long mult = (mults[right] - mult_left + MOD ) % MOD;
      long sum = sums[right] - sums[left - 1];

      long cand = (mult * sum) % MOD;
      answer[ai] = (int)cand;
    }
    return answer;
  }
}


public class MainClass {

  public record TC(string a, int[][] b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC("10203004", Parse2D("[[0,7],[1,3],[4,6]]")),
      new TC("1000", Parse2D("[[0,3],[1,1]]")),
      new TC("9876543210", Parse2D("[[0,9]]")),
    ];
    foreach (var tc in testcases) {
      var result = solution.SumAndMultiply(tc.a, tc.b);
      // PrintListNode(result);
      PrintArray(result);
      // Console.WriteLine(result);
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

