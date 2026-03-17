// 1727 Largest Submatrix With Rearrangements
// https://leetcode.com/problems/largest-submatrix-with-rearrangements/description/
// Difficulty: Medium
// Time Taken: 01:14:54
// Failed.


public class Solution {

  public int LargestSubmatrix(int[][] matrix) {
    int maxHeight = matrix.Length;
    int maxWidth = matrix[0].Length;

    List<int[]> rows = [matrix[0]];
    for(int y = 1; y < maxHeight; y++) {
      int[] row = new int[maxWidth];
      for(int x = 0; x < maxWidth; x++) {
        if (matrix[y][x] == 1) {
          row[x] = 1;
          if (rows[^1][x] >= 1) {
            row[x] += rows[^1][x];
          }
        }
      }
      rows.Add(row);
    }

    int bestArea = -1;
    foreach(var row in rows) {
      var sortedRow = row.ToList().OrderDescending().ToArray();
      for(int col = 0; col < row.Length; col++) {
        int candWidth = col + 1;
        int candHeight = sortedRow[col];
        int candArea = candWidth * candHeight;
        bestArea = Math.Max(bestArea, candArea);
      }
    }

    return bestArea;
  }
}

public class MainClass {
  public static void Main(string[] args) {
    testcase1();
    testcase2();
    testcase3();
  }

  public static void PrintArray<T>(T[] array) {
    Console.WriteLine($"[{string.Join(", ", array)}]");
  }

  public static void testcase1() {
    Solution solution = new Solution();
    // [[0,0,1],[1,1,1],[1,0,1]]
    int result = solution.LargestSubmatrix(
      new int[][] {
        new int[] {0,0,1},
        new int[] {1,1,1},
        new int[] {1,0,1}
      }

    );
    Console.WriteLine($"{result}");
  }

  public static void testcase2() {
    Solution solution = new Solution();
    // [[1,0,1,0,1]]
    int result = solution.LargestSubmatrix(
      new int[][] {
        new int[] {1,0,1,0,1}
      }
    );
    Console.WriteLine($"{result}");
  }

  public static void testcase3() {
    Solution solution = new Solution();
    // [[1,1,0],[1,0,1]]
    int result = solution.LargestSubmatrix(
      new int[][] {
        new int[] {1,1,0},
        new int[] {1,0,1}
      }
    );
    // Console.WriteLine($"{result}"); // "c"
    Console.WriteLine($"{result}");
  }
};

