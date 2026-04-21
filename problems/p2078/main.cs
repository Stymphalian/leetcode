// 2078 Two Furthest Houses With Different Colors
// https://leetcode.com/problems/two-furthest-houses-with-different-colors/description/
// Difficulty: Easy
// Time Taken: 00:07:43


public class Solution {
  public int MaxDistance(int[] colors) {
    Dictionary<int, int> lefts = [];
    Dictionary<int, int> rights = [];
    for(int index = 0; index < colors.Length; index++) {
      int color = colors[index];
      if (!lefts.ContainsKey(color)) {
        lefts[color] = index;
      }
      rights[color] = index;
    }

    int best = 0;
    foreach(var (color, left) in lefts) {
      foreach(var (other, right) in rights) {
        if (color == other) {continue;}
        best = Math.Max(Math.Abs(right - left), best);
      }
    }
    return best;
  }
}

public class MainClass {

  public record TC(int[] n);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([1,1,1,6,1,1,1]),
      new TC([1,8,3,8,3]),
      new TC([0,1]),
    ];
    foreach (var tc in testcases) {
      var result = solution.MaxDistance(tc.n);
      // PrintArray(result);
      Console.WriteLine(result);
    }
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


