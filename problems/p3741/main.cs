// 3741 Minimum Distance Between Three Equal Elements II
// https://leetcode.com/problems/minimum-distance-between-three-equal-elements-ii/description/
// Difficulty: Medium
// Time Taken: 00:10:00

public class Solution {
  public int MinimumDistance(int[] nums) {
    Dictionary<int, List<int>> groups = [];
    for(int index = 0; index < nums.Length; index++) {
      int value = nums[index];
      if (!groups.ContainsKey(value)) {
        groups[value] = [];
      }
      groups[value].Add(index);
    }

    int best = int.MaxValue;
    foreach(var (_, group) in groups) {
      if (group.Count <= 2) {continue;}
      for(int index = 0; index < group.Count-2; index++) {
        int i = group[index];
        int j = group[index+1];
        int k = group[index+2];
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

