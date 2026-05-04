// 48 Rotate Image
// https://leetcode.com/problems/rotate-image/description/
// Difficulty: Medium
// Time Taken: 00:45:16

public class Solution {
  public void Rotate(int[][] matrix) {
    int rows = matrix.Length;
    int cols = matrix[0].Length;

    void swap(int row, int col, int row1, int col1) {
      int tmp = matrix[row][col];
      matrix[row][col] = matrix[row1][col1];
      matrix[row1][col1] = tmp;
    }

    void ReverseRow(int row) {
      int left = 0;
      int right = cols -1;
      while (left < right) {
        swap(row, left, row, right);
        left++;
        right--;
      }
    }

    // Swap back to front
    for(int row = 0; row < rows; row++) {
      ReverseRow(row);
    }
    
    // Rotate in place
    for(int row = 0; row < rows; row++) {
      int end = cols - row - 1;
      for(int col = 0; col <= end; col++) {
        int row1 = rows - col - 1;
        int col1 = end;
        swap(row, col, row1, col1);
      }
    }
  }
}

public class MainClass {

  public record TC(int[][] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(Parse2D("[[1,2,3],[4,5,6],[7,8,9]]")),
      new TC(Parse2D("[[5,1,9,11],[2,4,8,10],[13,3,6,7],[15,14,12,16]]")),
    ];
    foreach (var tc in testcases) {
      solution.Rotate(tc.a);
      PrintArray2D(tc.a);
      // Console.WriteLine(tc.a);
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

