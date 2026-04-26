// 1559 Detect Cycles in 2D Grid
// https://leetcode.com/problems/detect-cycles-in-2d-grid/description/
// Difficulty: Medium
// Time Taken: 00:28:41

public class Solution {

  public static readonly (int, int)[] DIRS = [(1, 0), (0, 1), (-1, 0), (0, -1)];

  public bool DFS(char[][] grid, (int, int) pos, (int, int) from, HashSet<(int, int)> visited) {
    int height = grid.Length;
    int width = grid[0].Length;
    int x = pos.Item1;
    int y = pos.Item2;
    visited.Add((x, y));

    foreach (var dir in DIRS) {
      int x1 = x + dir.Item1;
      int y1 = y + dir.Item2;
      if (x1 < 0 || y1 < 0 || x1 >= width || y1 >= height) { continue; }
      if ((x1, y1) == from) { continue; }
      if (grid[y1][x1] != grid[y][x]) { continue; }

      if (visited.Contains((x1, y1))) {
        return true;
      }
      bool has = DFS(grid, (x1, y1), (x, y), visited);
      if (has) { return true; }
    }

    return false;
  }

  public bool ContainsCycle(char[][] grid) {
    int height = grid.Length;
    int width = grid[0].Length;
    HashSet<(int, int)> visited = [];
    for (int row = 0; row < height; row++) {
      for (int col = 0; col < width; col++) {
        if (visited.Contains((col, row))) {
          continue;
        }
        bool got = DFS(grid, (col, row), (col, row), visited);
        if (got) { return true; }
      }
    }

    return false;
  }
}
