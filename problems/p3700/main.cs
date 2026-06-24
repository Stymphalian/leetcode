// 3700 Number of ZigZag Arrays II
// https://leetcode.com/problems/number-of-zigzag-arrays-ii/description/
// Difficulty: Hard
// Time Taken: 02:19:07

// Well atleast I learned something new.
// but fuck how would I think of this?


public class Solution {

  public const int UP = 0;
  public const int DOWN = 1;
  public const int MOD = 1_000_000_007;

  public int[][] MatrixMultiply(int[][] A, int[][] B) {
    // A and B are square and of the same size
    int a_rows = A.Length;
    int a_cols = A[0].Length;
    // int b_rows = B.Length;
    int b_cols = B[0].Length;

    int[][] C = new int[a_rows][];
    for (int idx = 0; idx < a_rows; idx++) { C[idx] = new int[b_cols]; }

    for (int a_row = 0; a_row < a_rows; a_row++) {
      for (int b_col = 0; b_col < b_cols; b_col++) {
        long accumulation = 0;
        for (int a_col = 0; a_col < a_cols; a_col++) {
          long a = A[a_row][a_col];
          long b = B[a_col][b_col];
          long c = (a * b) % MOD;
          accumulation = (accumulation + c) % MOD;
        }

        C[a_row][b_col] = (int)accumulation;
      }
    }

    return C;
  }

  public int[][] MatrixExponent(int[][] M, int power) {

    int n = M.Length;
    int[][] result = new int[n][];
    for (int idx = 0; idx < n; idx++) { result[idx] = new int[n]; }
    // Make identity matrix
    for (int diag = 0; diag < n; diag++) { result[diag][diag] = 1; }

    while (power > 0) {
      if (power % 2 == 1) {
        result = MatrixMultiply(result, M);
      }
      M = MatrixMultiply(M, M);
      power /= 2;
    }
    return result;
  }

  public int ZigZagArrays(int n, int l, int r) {
    int m = r - l + 1;
    int m2 = 2 * m;

    // Make initial state vector
    int[][] V = new int[m2][];
    for (int idx = 0; idx < m2; idx++) { V[idx] = [1]; }

    // Create initial transition vector
    int[][] T = new int[m2][];
    for (int idx = 0; idx < m2; idx++) { T[idx] = new int[m2]; }
    for (int row = 0; row < m; row++) {
      // ups
      for (int col = 0; col < row; col++) {
        T[m + row][col] = 1;
      }
      // down 
      for (int col = row + 1; col < m; col++) {
        T[row][m + col] = 1;
      }
    }

    // matrix exponentiation up to n
    int[][] T2 = MatrixExponent(T, n - 1);

    // apply T * V and read of nth value
    int[][] A = MatrixMultiply(T2, V);
    int answer = 0;
    for (int idx = 0; idx < A.Length; idx++) {
      answer = (answer + A[idx][0]) % MOD;
    }
    return answer;
  }
}


public class MainClass {

  public record TC(int a, int b, int c);


  public static void Main(string[] args) {
    // TestMatrixMultiply();
    Solution solution = new();

    List<TC> testcases = [
      new TC(3, 4, 5), // 2
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

