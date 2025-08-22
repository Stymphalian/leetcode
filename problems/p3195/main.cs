
// 3195 Find the Mininum Area to Cover All Ones I
// https://leetcode.com/problems/find-the-minimum-area-to-cover-all-ones-i/description
// Difficulty: Medium??
// Time Taken: 00:20:15

using System.Data;
using System.IO.Compression;
using System.Reflection;
public class Solution {

    public void PrintArray(int[][] grid) {
        int rows = grid.Length;
        int cols = grid[0].Length;
        for (int row = 0; row < rows; row++) {
            Console.WriteLine(string.Join(", ", grid[row]));
        }
    }

    public int MinimumArea(int[][] grid) {
        PrintArray(grid);
        int rows = grid.Length;
        int cols = grid[0].Length;

        int left = cols - 1;
        int right = 0;
        int top = rows-1;
        int bottom = 0;
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                if (grid[row][col] == 0) continue;

                top = Math.Min(top, row);
                bottom = Math.Max(bottom, row);
                left = Math.Min(left, col);
                right = Math.Max(right, col);
            }
        }
        int width = right - left + 1;
        int height = bottom - top + 1;
        return width * height;
    }
}

public class MainClass {
    record Case(int[,] nums, int rows, int cols);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new int[,] { {0, 1, 0}, {1, 0, 1} }, 2, 3), // 6
            new Case(new int[,] { {1, 0} , {0, 0}}, 2, 2), // 1
        };

        foreach (var c in cases) {
            int[][] newNums = new int[c.rows][];
            for (int row = 0; row < c.rows; row++) {
                newNums[row] = new int[c.cols];
                for (int col = 0; col < c.cols; col++) {
                    newNums[row][col] = c.nums[row, col];
                }
            }
            Console.WriteLine(s.MinimumArea(newNums));
        }
    }
}

