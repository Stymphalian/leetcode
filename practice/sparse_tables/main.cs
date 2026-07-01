using System.Diagnostics;
using System.Text.Json.Serialization.Metadata;

public class Solution {

  public class SparseTable {
    public int[][] values;

    public SparseTable(int[] arr) {
      Build(arr);
    }

    private void Build(int[] arr) {
      int cols = arr.Length;
      Debug.Assert(cols >= 1);
      int rows = (int)Math.Log2(cols) + 1;

      values = new int[rows][];
      for (int row = 0; row < rows; row++) {
        values[row] = new int[cols];
      }
      Array.Copy(arr, values[0], values[0].Length);

      int size = 1;
      for (int row = 1; row < rows; row++) {

        int col_limit = cols - size;
        for (int col = 0; col < col_limit; col++) {
          int left = values[row - 1][col];
          int right = values[row - 1][col + size];
          int current = Math.Min(left, right);
          values[row][col] = current;
        }

        size *= 2;
      }
    }

    public int Range(int left, int right) {
      int size = right - left + 1;
      int answer = int.MaxValue;
      int col = left;
      while (size > 0) {
        int row = (int)Math.Log2(size);
        int power2 = 1 << row;

        int value = values[row][col];
        answer = Math.Min(value, answer);
        size -= power2;
        col += power2;
      }
      return answer;
    }

    public void Update(int idx, int value) {
      int cols = values[0].Length;
      int rows = values.Length;
      int size = 1;
      int left = Math.Max(idx - size, 0);
      int right = idx;
      values[0][idx] = value;

      for (int row = 1; row < rows; row++) {

        int col_limit = Math.Min(cols - size, right+1);
        for (int col = left; col < col_limit; col++) {
          int leftValue = values[row - 1][col];
          int rightValue = values[row - 1][col + size];
          values[row][col] = Math.Min(leftValue, rightValue);
        }

        size *= 2;
        left = Math.Max(left - size, 0);
      }
    }
  }

  public int Brute(int[] arr, int left, int right) {
    int answer = int.MaxValue;
    for (int idx = left; idx <= right; idx++) {
      answer = Math.Min(arr[idx], answer);
    }
    return answer;
  }

  public void Run() {
    int numTestRuns = 5;
    int numUpdates = 10;
    int maxArraySize = 100;
    int arrayRange = 500;
    var rng = new Random(0);

    for (int tc = 0; tc < numTestRuns; tc++) {
      for (int arraySize = 1; arraySize <= maxArraySize; arraySize++) {
        // Create the array 
        int[] arr1 = new int[arraySize];
        for (int idx = 0; idx < arraySize; idx++) {
          arr1[idx] = rng.Next() % arrayRange;
        }

        SparseTable sparse = new SparseTable(arr1);

        for (int update = 0; update < numUpdates; update++) {
          int target = rng.Next() % arr1.Length;
          int newVal = rng.Next() % arrayRange;
          arr1[target] = newVal;
          sparse.Update(target, newVal);

          for (int left = 0; left < arraySize; left++) {
            for (int right = left; right < arraySize; right++) {
              int want = Brute(arr1, left, right);
              int got = sparse.Range(left, right);
              Debug.Assert(want == got, $"want {want} but got {got}");
            }
          }
        }
      }

    }
  }
}


public class MainClass {

  public record TC(int[][] a);

  public static void Main(string[] args) {
    Solution solution = new();
    solution.Run();

    List<TC> testcases = [
      new TC(Parse2D("[[1,0,0],[0,0,0],[0,0,1]]")), // 0
      new TC(Parse2D("[[0,0,1],[0,0,0],[0,0,0]]")), // 2
      new TC(Parse2D("[[0,0,0,1],[0,0,0,0],[0,0,0,0],[1,0,0,0]]")), // 2
      new TC(Parse2D("[[0,1,1],[0,1,1],[0,1,1]]")), // 0
    ];
    foreach (var tc in testcases) {
      // var result = solution.MaximumSafenessFactor(tc.a);
      // PrintListNode(result);
      // PrintArray(result);
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

