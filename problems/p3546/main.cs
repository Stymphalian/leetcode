// 3546 Equal Sum Grid Partition I
// https://leetcode.com/problems/equal-sum-grid-partition-i/description/
// Difficulty: Medium
// Time Taken: 00:14:38

public class Solution {
  public bool CanPartitionGrid(int[][] grid) {
    int height = grid.Length;
    int width = grid[0].Length;

    long[][] sums = new long[height][];
    for(int y = 0; y < height; y++) {
      sums[y] = new long[width];
    }
    for(int y = 0; y < height; y++) {
      for(int x = 0; x < width; x++) {
        long top = (y-1 >= 0) ? sums[y-1][x] : 0;
        long left = (x-1 >= 0) ? sums[y][x-1] : 0;
        long topleft = (y-1 >= 0 && x-1 >= 0) ? sums[y-1][x-1] : 0;
        sums[y][x] = top + left - topleft + grid[y][x];
      }
    }

    for(int y = 1; y < height; y++) {
      // Make a horiztonal cut
      long top = sums[y-1][width-1];
      long bottom = sums[height-1][width-1] - top;
      if (top == bottom) {
        return true;
      }
    }
    for(int x = 1; x < width; x++) {
      // make a vertical cut
      long left = sums[height-1][x-1];
      long right = sums[height-1][width-1] - left;
      if (left == right) {
        return true;
      }
    }
    return false;
  }
}

public class MainClass {

  public record TestCase(int[][] mat);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      // new TestCase(ParseArray2D("[[1,4],[2,3]]")),
      // new TestCase(ParseArray2D("[[1,3],[2,4]]")),
      new TestCase(ParseArray2D("[[54756,54756]]")),
    };

    foreach (var tc in testcases) {
      var result = solution.CanPartitionGrid(tc.mat);
      Console.WriteLine(result);
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


