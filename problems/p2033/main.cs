// 2033 Minimum Operations to Make a Uni-Value Grid
// https://leetcode.com/problems/minimum-operations-to-make-a-uni-value-grid/description/
// Difficulty: Medium
// Time Taken: 00:27:34



public class Solution {
  public int MinOperations(int[][] grid, int x) {
    List<int> elems = [];
    int side = 0;
    for (int row = 0; row < grid.Length; row++) {
      for(int col = 0; col < grid[0].Length; col++) {
        elems.Add(grid[row][col]);
        if (grid[row][col] %2 == 0) {
          side += 1;
        } else {
          side -= 1;
        }
      }
    }
    elems.Sort();
    if (elems.Count == 1) {
      return 0;
    }


    (int, bool) CountOperations(int target) {
      int count = 0;
      for(int index = 0; index < elems.Count; index++) {
        if (index == target) {continue;}
        int dist = Math.Abs(elems[target] - elems[index]);
        if (dist % x != 0) {
          return (0, false);
        }
        // Debug.Assert(dist % x == 0);
        int ops = dist / x;
        count += ops;
      }
      return (count, true);
    }

    int best;
    int left = elems.Count / 2 - 1;
    int right = elems.Count / 2;
    var (leftBest, leftOk) = CountOperations(left);
    var (rightBest, rightOk) = CountOperations(right);
    best = Math.Min(leftBest, rightBest);
    if (leftOk || rightOk) {
      return best;
    } else {
      return -1;
    }
  }
}



public class MainClass {

  public record TC(int[][] a, int b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(Parse2D("[[2,4],[6,8]]"), 2),
      new TC(Parse2D("[[1,5],[2,3]]"), 1),
      new TC(Parse2D("[[1,2],[3,4]]"), 2),
      new TC(Parse2D("[[931,128],[639,712]]"), 73),
    ];
    foreach (var tc in testcases) {
      var result = solution.MinOperations(tc.a, tc.b);
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

