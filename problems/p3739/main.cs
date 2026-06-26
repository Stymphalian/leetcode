// 3739 Count Subarrays With Majority Element II
// https://leetcode.com/problems/count-subarrays-with-majority-element-ii/description/
// Difficulty: Hard
// Time Taken: 02:45:06

// nope. not smart enough.

public class Solution {

  public class Fenwick {
    public int[] values;

    public Fenwick(int n) {
      values = new int[n + 1];
    }

    // void make(int[] arr) {
    //   int n = arr.Length;
    //   values = new int[n+1];
    //   for(int idx = 0; idx < n; idx++) {
    //     values[idx+1] = arr[idx];
    //   }

    //   for(int idx = 1; idx <= n; idx++) {
    //     int parent = idx + (idx & -idx);
    //     values[parent] += values[idx];
    //   }
    // }

    public void update(int index, int delta) {
      index += 1; // 1 indexed
      while (index < values.Length) {
        values[index] += delta;
        index += (index & -index);
      }
    }

    public int sum(int index) {
      index += 1;  // 1 indexed
      int count = 0;
      while (index > 0) {
        count += values[index];
        index -= (index & -index);
      }
      return count;
    }
  };

  public long CountMajoritySubarrays(int[] nums, int target) {
    int n = nums.Length;
    Fenwick fenwick = new Fenwick(2 * n);


    long answer = 0;
    int prefixSum = 0;
    fenwick.update(0 + n, 1); // initialize the fenwick tree to track a 0 sum.

    for (int idx = 0; idx < n; idx++) {
      prefixSum += (nums[idx] == target) ? 1 : -1;

      // shifted because prefixSum can be -n -> n, and we need to index into the 
      // fenwick tree (which must be a number 0 to 2*n)
      int shifted = prefixSum + n;

      // check how many subarrays (index i) before the current index (j) have a 
      // prefixSum which is less than the current prefixSum.
      // This satisfies the condition sum[j] > sum[i]
      int countLess = fenwick.sum(shifted - 1); 
      answer += countLess;
      fenwick.update(shifted, 1);
    }

    return answer;
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

