// 3070 Count Submatrices with Top-Left Element and Sum Less Than k
// https://leetcode.com/problems/count-submatrices-with-top-left-element-and-sum-less-than-k/description/
// Difficulty: Medium
// Time Taken: 00;10:54

public class Solution {
  public int CountSubmatrices(int[][] grid, int k) {
    int[][] dp = new int[grid.Length][];
    for (int i = 0; i < grid.Length; i++) {
      dp[i] = new int[grid[i].Length];
    }

    int height = grid.Length;
    int width = grid[0].Length;
    int best = 0;
    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        int topleft = (y-1 >= 0 && x-1 >= 0) ? dp[y-1][x-1] : 0;
        int top = y-1 >= 0 ? dp[y-1][x] : 0;
        int left = x-1 >= 0 ? dp[y][x-1]: 0;
        dp[y][x] = top + left - topleft + grid[y][x];
        if (dp[y][x] <= k) {
          best += 1;
        }
      }
    }

    return best;
  }
}


public class MainClass {
  public static void Main(string[] args) {
    testcase1();
    testcase2();
  }

  public static void PrintArray<T>(T[] array) {
    Console.WriteLine($"[{string.Join(", ", array)}]");
  }

  public static void testcase1() {
    Solution solution = new Solution();
    // [[7,6,3],[6,6,1]]
    int result = solution.CountSubmatrices(
      new int[][] {
        new int[] {7,6,3},
        new int[] {6,6,1},
      },
      18
    );
    Console.WriteLine($"{result}");
  }

  public static void testcase2() {
    Solution solution = new Solution();
    // [[7,2,9],[1,5,0],[2,6,6]]
    int result = solution.CountSubmatrices(
      new int[][] {
        new int[] {7,2,9},
        new int[] {1,5,0},
        new int[] {2,6,6},
      },
      20
    );
    Console.WriteLine($"{result}");
  }
};

