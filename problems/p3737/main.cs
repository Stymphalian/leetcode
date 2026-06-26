// 3737 Count Subarrays With Majority Element I
// https://leetcode.com/problems/count-subarrays-with-majority-element-i/description/
// Difficulty: Medium
// Time Taken: 00:36:19




public class Solution {
  public int CountMajoritySubarrays(int[] nums, int target) {
    int n = nums.Length;
    long answer = 0;

    long[] prev = new long[n];
    long[] current = new long[n];
    long[] next = new long[n];
    for(int idx = 0; idx < n; idx++) {
      prev[idx] = 0;
      current[idx] = (nums[idx] == target) ? 1 : 0;
      answer += current[idx];
    }

    for(int len = 2; len <= n; len++) {
      int limit = n - len + 1;
      for(int idx = 0; idx < limit; idx++) {
        long a = current[idx];
        long b = (idx+1 < n) ? current[idx+1] : 0;  
        long c = (idx+1 < n) ? prev[idx+1] : 0;
        long count = (a+b-c);
        next[idx] = count;
        if (count > len/2) {
          answer += 1;
        }
      }
      (prev, current, next) = (current, next, prev);
    }

    return (int) answer;
  }
}


public class MainClass {

  public record TC(int[] a, int b);


  public static void Main(string[] args) {
    // TestMatrixMultiply();
    Solution solution = new();

    List<TC> testcases = [
      new TC([1,2,2,3], 2),
      new TC([1,1,1,1], 1),
      new TC([1,2,3], 4),
    ];
    foreach (var tc in testcases) {
      var result = solution.CountMajoritySubarrays(tc.a, tc.b);
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

