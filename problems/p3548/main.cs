// 3548 Equal Sum Grid Partition II
// https://leetcode.com/problems/equal-sum-grid-partition-ii/description/
// Difficulty: Hard
// Time Taken: 01:14:22

// Use rotations to simplify the code

public class Solution {
  public bool CanPartitionGrid(int[][] grid) {
    int height = grid.Length;
    int width = grid[0].Length;

    long[][] sums = new long[height][];
    Dictionary<long, List<(int,int)>> seen = [];
    for (int y = 0; y < height; y++) {
      sums[y] = new long[width];
    }
    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        long top = (y - 1 >= 0) ? sums[y - 1][x] : 0;
        long left = (x - 1 >= 0) ? sums[y][x - 1] : 0;
        long topleft = (y - 1 >= 0 && x - 1 >= 0) ? sums[y - 1][x - 1] : 0;
        sums[y][x] = top + left - topleft + grid[y][x];
        if (!seen.ContainsKey(grid[y][x])) {
          seen.Add(grid[y][x], []);
        }
        seen[grid[y][x]].Add((y,x));
      }
    }

    for (int y = 1; y < height; y++) {
      // Make a horiztonal cut
      long top = sums[y - 1][width - 1];
      long bottom = sums[height - 1][width - 1] - top;
      if (top == bottom) {
        return true;
      }

      long diff = Math.Abs(top - bottom);
      if (!seen.ContainsKey(diff)) {
        continue;
      }

      if (top > bottom) {
        // look for the diff value in the top
        foreach(var (y1,x1) in seen[diff]) {
          if (y1 < y) {
            if (y == 1) {
              if (x1 == 0 || x1 == width-1) {
                return true;
              }
            } else if (width == 1) {
              if (y1 == 0 || y1 == y) {
                return true;
              }
            } else {
              return true;
            }
          }
        }
      } else {
        // Look for the diff value in the bottom
        foreach(var (y1,x1) in seen[diff]) {
          if (y1 >= y) {
            if (y == height-1) {
              if (x1 == 0 || x1 == width-1) {
                return true;
              }
            } else if (width == 1) {
              if (y1 == y || y1 == height-1) {
                return true;
              }
            } else {
              return true;
            }
          }
        }
      }
    }

    for (int x = 1; x < width; x++) {
      // make a vertical cut
      long left = sums[height - 1][x - 1];
      long right = sums[height - 1][width - 1] - left;
      if (left == right) {
        return true;
      }
      long diff = Math.Abs(left - right);
      if (!seen.ContainsKey(diff)) {
        continue;
      }

      if (left > right) {
        // look for the diff value in the left
        foreach(var (y1,x1) in seen[diff]) {
          if (x1 < x) {
            if (x == 1) {
              if (y1 == 0 || y1 == height-1) {
                return true;
              }
            } else if (height == 1) {
              if (x1 == 0 || x1 == x) {
                return true;
              }
            } else {
              return true;
            }
          }
        }
      } else {
        // Look for the diff value in the right
        foreach(var (y1,x1) in seen[diff]) {
          if (x1 >= x) {
            if (x == width-1) {
              if (y1 == 0 || y1 == height-1) {
                return true;
              }
            } else if (height == 1) {
              if (x1 == x || x1 == width-1) {
                return true;
              }
            } else {
              return true;
            }
          }
        }
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
      new TestCase(ParseArray2D("[[1,4],[2,3]]")), // true
      new TestCase(ParseArray2D("[[1,2],[3,4]]")), // true
      new TestCase(ParseArray2D("[[1,2,4],[2,3,5]]")), // false
      new TestCase(ParseArray2D("[[4,1,8],[3,2,6]]")), // false
      new TestCase(ParseArray2D("[[10,5,4,5]]")), // false
      new TestCase(ParseArray2D("[[10],[5],[4],[5]]")), // false
      new TestCase(ParseArray2D("[[5,5,6,2,2,2]]")), // true
      new TestCase(ParseArray2D("[[100000],[86218],[100000]]")), // true
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


