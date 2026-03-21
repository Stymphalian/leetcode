// 3643 Flip Square Submatrix Vertically
// https://leetcode.com/problems/flip-square-submatrix-vertically/description/
// Difficulty: Easy
// Time Taken: 00:13:50

using System.Runtime.Serialization.Formatters;

public class Solution {
  public int[][] ReverseSubmatrix(int[][] grid, int y, int x, int k) {

    void SwapRows(int y0, int y1) {
      for(int u = x; u < x + k; u++) {
        (grid[y0][u], grid[y1][u]) = (grid[y1][u], grid[y0][u]);
      }
    }

    int top = y;
    int bot = y + k-1;
    while (top < bot) {
      SwapRows(top, bot);
      top++;
      bot--;
    }

    return grid;
  }
}

public class MainClass {

  public record TestCase(int[][] grid, int x, int y, int k);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      // [[1,2,3,4],[5,6,7,8],[9,10,11,12],[13,14,15,16]], 1, 0, 3
      new TestCase(
        ParseArray2D("[[1,2,3,4],[5,6,7,8],[9,10,11,12],[13,14,15,16]]"),
        1,
        0,
        3
      ),
      // [[3,4,2,3],[2,3,4,2]], 0, 2, 2
      new(
        ParseArray2D("[[3,4,2,3],[2,3,4,2]]"),
        0,
        2,
        2
      )
    };

    foreach (var tc in testcases) {
      var result = solution.ReverseSubmatrix(tc.grid, tc.x, tc.y, tc.k);
      PrintArray2D(result);
    }
  }


  public static int[] ParseArray(string input) {
    var array = Array.ConvertAll(input.Trim('[', ']').Split(','), int.Parse);
    return array;
  }

  public static int[][] ParseArray2D(string input) {
    var rows = input.Trim('[', ']').Split("],[");
    var array = new int[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
      array[i] = Array.ConvertAll(rows[i].Split(','), int.Parse);
    }
    return array;
  }

  public static void PrintArray<T>(T[] array, string prefix = "") {
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



