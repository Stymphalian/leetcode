// 1914 Cyclically Rotating a Grid
// https://leetcode.com/problems/cyclically-rotating-a-grid/description/
// Difficulty: Medium
// Time Taken: 01;46:09


public class Solution {
  public (int, int) IndexToCoord(int index, int rows, int cols) {
    int height = rows - 2;
    int bucket1 = cols;
    int bucket2 = bucket1 + height;
    int bucket3 = bucket2 + cols;
    int first_column = 0;
    int last_column = cols - 1;
    int first_row = 0;
    int last_row = rows - 1;
    if (index < bucket1) {
      return (index, first_row);
    } else if (bucket1 <= index && index < bucket2) {
      int index2 = index - bucket1 + 1;
      return (last_column, index2);
    } else if (bucket2 <= index && index < bucket3) {
      int index2 = index - bucket2;
      int index3 = cols - index2 - 1;
      return (index3, last_row);
    } else { // bucket3 < index && index < bucket4
      int index2 = index - bucket3 + 1;
      int index3 = height - index2 + 1;
      return (first_column, index3);
    }
  }

  // public int CoordToIndex(int x, int y, int rows, int cols) {
  //   int height = rows - 2;
  //   int bucket1 = cols;
  //   int bucket2 = bucket1 + height;
  //   int bucket3 = bucket2 + cols;
  //   int last_column = cols - 1;
  //   int first_row = 0;
  //   int last_row = rows - 1;
  //   if (y == first_row) {
  //     return x;
  //   } else if (y == last_row) {
  //     return cols + bucket2 -1 - x;
  //   } else if (x == last_column) {
  //     return y - 1 + bucket1;
  //   } else {
  //     return rows - 2 + bucket3 - y;
  //   }
  // }

  public void RotateOuter(int[][] grid, int k, int xoffset, int yoffset, int rows, int cols, int[][] output) {
    if (rows == 0 || cols == 0) {
      return;
    }
    int size = 2 * rows + 2 * (cols - 2);
    int k2 = k % size;
    for (int index = 0; index < size; index++) {
      var (x,y) = IndexToCoord(index, rows, cols);    
      int index2 = (index + size - k2) % size;
      var (x2,y2) = IndexToCoord(index2, rows, cols);

      x += xoffset;
      y += yoffset;
      x2 += xoffset;
      y2 += yoffset;
      output[y2][x2] = grid[y][x];
    }

    int new_rows = rows - 2;
    int new_cols = cols - 2;
    RotateOuter(grid, k, xoffset + 1, yoffset + 1, new_rows, new_cols, output);
  }

  public int[][] RotateGrid(int[][] grid, int k) {
    int rows = grid.Length;
    int cols = grid[0].Length;
    int[][] output = new int[rows][];
    for (int idx = 0; idx < rows; idx++) {
      output[idx] = new int[cols];
    }
    RotateOuter(grid, k, 0, 0, rows, cols, output);
    return output;
  }
}

public class MainClass {

  public record TC(int[][] a, int b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(Parse2D("[[40,10],[30,20]]"), 1),
      new TC(Parse2D("[[1,2,3,4],[5,6,7,8],[9,10,11,12],[13,14,15,16]]"), 2)
    ];
    foreach (var tc in testcases) {
      var result = solution.RotateGrid(tc.a, tc.b);
      PrintArray2D(result);
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

