// 1391 Check if There is a Valid Path in a Grid
// https://leetcode.com/problems/check-if-there-is-a-valid-path-in-a-grid/description/
// Difficulty: Medium
// Time Taken: 01:02;54

// Got stupid brained.
// Just use a transition matrix, with a pre-defiend starting direction given the
// current state of the road. Handle edge case of road 3,4,5,6 by trying both paths
// only for the starting position.

public class Solution {

  public bool HasValidPath(int[][] grid) {
    int height = grid.Length;
    int width = grid[0].Length;

    bool CanMove(int current, int next, int x, int y, int x1, int y1) {
      if (current == 1) {
        if (next == 1) { return true; }
        if (next == 3 && x1 > x) { return true; }
        if (next == 5 && x1 > x) { return true; }
        if (next == 4 && x1 < x) { return true; }
        if (next == 6 && x1 < x) { return true; }
      } else if (current == 2) {
        if (next == 2) { return true; }
        if (next == 3 && y1 < y) { return true; }
        if (next == 4 && y1 < y) { return true; }
        if (next == 5 && y1 > y) { return true; }
        if (next == 6 && y1 > y) { return true; }
      } else if (current == 3) {
        if (next == 1 && x1 < x) { return true; }
        if (next == 2 && y1 > y) { return true; }
        if (next == 4 && x1 < x) { return true; }
        if (next == 5 && y1 > y) { return true; }
        if (next == 6) { return true; }
      } else if (current == 4) {
        if (next == 1 && x1 > x) { return true; }
        if (next == 2 && y1 > y) { return true; }
        if (next == 3 && x1 > x) { return true; }
        if (next == 5) { return true; }
        if (next == 6 && y1 > y) { return true; }
      } else if (current == 5) {
        if (next == 1 && x1 < x) { return true; }
        if (next == 2 && y1 < y) { return true; }
        if (next == 3 && y1 < y) { return true; }
        if (next == 4) { return true; }
        if (next == 6 && x1 < x) { return true; }
      } else { // if (current == 6)
        if (next == 1 && x1 > x) { return true; }
        if (next == 2 && y1 < y) { return true; }
        if (next == 3) { return true; }
        if (next == 4 && y1 < y) { return true; }
        if (next == 5 && x1 > x) { return true; }
      }
      return false;
    }

    List<(int, int)> GetNext(int x, int y) {
      int current = grid[y][x];
      if (current == 1) {
        return [(x + 1, y), (x - 1, y)];
      } else if (current == 2) {
        return [(x, y - 1), (x, y + 1)];
      } else if (current == 3) {
        return [(x - 1, y), (x, y + 1)];
      } else if (current == 4) {
        return [(x + 1, y), (x, y + 1)];
      } else if (current == 5) {
        return [(x - 1, y), (x, y - 1)];
      } else { // if (current == 6)
        return [(x, y - 1), (x + 1, y)];
      }
    }

    List<(int, int)> stack = [(0, 0)];
    HashSet<(int, int)> visited = [];
    while (stack.Count > 0) {
      // next positions
      var (x, y) = stack[stack.Count - 1];
      stack.RemoveAt(stack.Count - 1);
    
      if (x == width - 1 && y == height - 1) { return true; }
      if (visited.Contains((x, y))) { continue; }
      visited.Add((x,y));

      var nextPositions = GetNext(x, y)
        .Where(a => {
          return a.Item1 >= 0 && a.Item2 >= 0 && a.Item1 < width && a.Item2 < height;
        })
        .Where(a => {
          return CanMove(grid[y][x], grid[a.Item2][a.Item1], x, y, a.Item1, a.Item2);
        });
      foreach (var pos in nextPositions) {
        stack.Add(pos);
      }
    }
    return false;
  }
}

public class MainClass {

  public record TC(int[][] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(Parse2D("[[2,4,3],[6,5,2]]")),
      new TC(Parse2D("[[1,2,1],[1,2,1]]")),
      new TC(Parse2D("[[1,1,2]]"))
    ];
    foreach (var tc in testcases) {
      var result = solution.HasValidPath(tc.a);
      // PrintArray(result);
      Console.WriteLine(result);
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

