// 3740 Minimum Distance Between Three Equal Elements I
// https://leetcode.com/problems/minimum-distance-between-three-equal-elements-i/description/
// Difficulty: Easy
// Time Taken: 00:18:40

public class Solution {
  public int MinimumDistance(int[] nums) {

    List<(int,int)> groups = nums
      .ToList()
      .Select((value, index) => (value, index))
      .OrderBy(x => x.value)
      .ThenBy(x => x.index)
      .ToList();
    int best = int.MaxValue;
    // window scan with groups of three
    for(int index = 0; index < groups.Count-2; index++) {
      var (vi, i) = groups[index];
      var (vj, j) = groups[index+1];
      var (vk, k) = groups[index+2];
      if (vi == vj && vj == vk) {
        int cand = (j-i) + (k-j) + (k-i);
        best = Math.Min(best, cand);
      }
    }      
    return best == int.MaxValue ? -1 : best;
  }
}

public class MainClass {

  public record TC(int[] e);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([1,2,1,1,3]),
      new TC([1,1,2,3,2,1,2]),
      new TC([1]),
      new TC([1,1,1,1]),
      new TC([5,3,5,5,5]),
    ];
    foreach (var tc in testcases) {
      var result = solution.MinimumDistance(tc.e);
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

