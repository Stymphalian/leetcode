// 3567 Minimum Absolute Difference in Sliding Submatrix
// https://leetcode.com/problems/minimum-absolute-difference-in-sliding-submatrix/description/
// Difficulty: Medium
// Time Taken: 00:34:40


public class Solution {
  public int[][] MinAbsDiff(int[][] grid, int k) {
    // Run a convolution of size k over the grid and record the min in each submat

    IEnumerable<int> Convolution(int x0, int y0) {
      for (int y = 0; y < k; y++) {
        int y1 = y0 + y;
        if (y1 >= grid.Length) { break; }
        for (int x = 0; x < k; x++) {
          int x1 = x0 + x;
          if (x1 >= grid[0].Length) { break; }
          yield return grid[y1][x1];
        }
      }
    }

    int GetMin(int x0, int y0) {
      int best = int.MaxValue;
      foreach (var n1 in Convolution(x0, y0)) {
        foreach (var n2 in Convolution(x0, y0)) {
          if (n1 != n2) {
            best = Math.Min(best, Math.Abs(n1 - n2));
          }
        }
      }

      if (best == int.MaxValue) {
        best = 0;
      }
      return best;
    }

    int height = grid.Length;
    int width = grid[0].Length;
    int ylimit = Math.Max(height - k + 1, 1);
    int xlimit = Math.Max(width - k + 1, 1);
    int[][] submat = new int[ylimit][];
    for (int y = 0; y < ylimit; y++) {
      submat[y] = new int[xlimit];
    }

    // for (int y = 0; y < ylimit; y++) {
    //   for (int x = 0; x < xlimit; x++) {
    //     submat[y][x] = GetMin(x, y);
    //   }
    // }

    return submat;
  }
}

public class MainClass {

  public record TestCase(int[][] v, int k);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      // [[1,8],[3,-2]], 2
      new TestCase(
        new int[][] {
          new int[] {1,8},
          new int[] {3,-2},
        },
        2
      ),
      // [[3,-1]], 1
      new TestCase(
        new int[][] {
          new int[] {3,-1},
        },
        1
      ),
      // [[1,-2,3],[2,3,5]], 2
      new TestCase(
        new int[][] {
          new int[] {1,-2,3},
          new int[] {2,3,5},
        },
        2
      ),
    };

    foreach (var testcase in testcases) {
      var result = solution.MinAbsDiff(testcase.v, testcase.k);
      PrintArray2D(result);
    }
  }

  public static void PrintArray<T>(T[] array, string prefix="") {
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


