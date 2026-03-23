// 1594 Maximum Non Negative Product in a Matrix
// https://leetcode.com/problems/maximum-non-negative-product-in-a-matrix/description/
// Difficulty: Medium
// Time Taken: 00:28:25


public class Solution {
  public static long MOD = 1000000007;

  public int MaxProductPath(int[][] grid) {
    (long,long)[][] dp = new (long,long)[grid.Length][];
    for(int y = 0; y < grid.Length; y++) {
      dp[y] = new (long, long)[grid[0].Length];
    }
    for(int x = 0; x < grid[0].Length; x++) {
      long prev = (x-1 >= 0) ? dp[0][x-1].Item1 : 1;
      dp[0][x] = (prev * grid[0][x], prev * grid[0][x]);
    }
    for(int y = 0; y < grid.Length; y++) {
      long prev = (y-1 >= 0) ? dp[y-1][0].Item1 : 1;
      dp[y][0] = (prev * grid[y][0], prev * grid[y][0]);
    }


    for(int y = 1; y < grid.Length; y++) {
      for(int x = 1; x < grid[0].Length; x++) {
        (long,long) top = (y-1 >= 0) ? dp[y-1][x] : (1,1);
        (long,long) left = (x-1 >= 0) ? dp[y][x-1] : (1,1);

        long Pos = Math.Max(
          Math.Max(grid[y][x] * top.Item1, grid[y][x] * top.Item2),
          Math.Max(grid[y][x] * left.Item1, grid[y][x] * left.Item2)
        );
        long Neg = Math.Min(
          Math.Min(grid[y][x] * top.Item1, grid[y][x] * top.Item2),
          Math.Min(grid[y][x] * left.Item1, grid[y][x] * left.Item2)
        );
        dp[y][x] = (Pos, Neg);
      }
    }

    long val = dp[grid.Length-1][grid[0].Length-1].Item1;
    if (val < 0) {
      return -1;
    } else {
      return (int) (val % MOD);
    }
  }
}

public class MainClass {

  public record TestCase(int[][] mat);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      // [[-1,-2,-3],[-2,-3,-3],[-3,-3,-2]]
      new TestCase(new int[][] {
        new int[] {-1, -2, -3},
        new int[] {-2, -3, -3},
        new int[] {-3, -3, -2}
      }),
      // [[1,-2,1],[1,-2,1],[3,-4,1]]
      new TestCase(new int[][] {
        new int[] {1, -2, 1},
        new int[] {1, -2, 1},
        new int[] {3, -4, 1}
      }),
      // [[1,3],[0,-4]]
      new TestCase(new int[][] {
        new int[] {1, 3},
        new int[] {0, -4}
      }),
      // [[1,4,4,0],[-2,0,0,1],[1,-1,1,1]]
      new TestCase(new int[][] {
        new int[] {1, 4, 4, 0},
        new int[] {-2, 0, 0, 1},
        new int[] {1, -1, 1, 1}
      }),
      // [[2,1,3,0,-3,3,-4,4,0,-4],[-4,-3,2,2,3,-3,1,-1,1,-2],[-2,0,-4,2,4,-3,-4,-1,3,4],[-1,0,1,0,-3,3,-2,-3,1,0],[0,-1,-2,0,-3,-4,0,3,-2,-2],[-4,-2,0,-1,0,-3,0,4,0,-3],[-3,-4,2,1,0,-4,2,-4,-1,-3],[3,-2,0,-4,1,0,1,-3,-1,-1],[3,-4,0,2,0,-2,2,-4,-2,4],[0,4,0,-3,-4,3,3,-1,-2,-2]]
      new TestCase(new int[][] {
        new int[] {2,1,3,0,-3,3,-4,4,0,-4},
        new int[] {-4,-3,2,2,3,-3,1,-1,1,-2},
        new int[] {-2,0,-4,2,4,-3,-4,-1,3,4},
        new int[] {-1,0,1,0,-3,3,-2,-3,1,0},
        new int[] {0,-1,-2,0,-3,-4,0,3,-2,-2},
        new int[] {-4,-2,0,-1,0,-3,0,4,0,-3},
        new int[] {-3,-4,2,1,0,-4,2,-4,-1,-3},
        new int[] {3,-2,0,-4,1,0,1,-3,-1,-1},
        new int[] {3,-4,0,2,0,-2,2,-4,-2,4},
        new int[] {0,4,0,-3,-4,3,3,-1,-2,-2}
      }),
    };

    foreach (var tc in testcases) {
      var result = solution.MaxProductPath(tc.mat);
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

