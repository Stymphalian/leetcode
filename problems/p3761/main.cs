// 3761 Minimum Absolute Distance Between Mirror Pairs
// https://leetcode.com/problems/minimum-absolute-distance-between-mirror-pairs/description/
// Difficulty: Medium
// Time Taken: 00:29:59


public class Solution {
  public int MinMirrorPairDistance(int[] nums) {
    // Preprocess the nums for quicker lookups
    Dictionary<int,int> refs = [];
    int reverseInt(int num) {
      int reverse = 0;
      while (num > 0) {
        int digit = num % 10;
        reverse *= 10;
        reverse += digit;
        num /= 10;
      }
      reverse += num;
      return reverse;
    }

    int best = int.MaxValue;
    for(int index = 0; index < nums.Length; index++) {
      int query = nums[index];
      if (refs.ContainsKey(query)) {
        int other = refs[query];
        best = Math.Min(Math.Abs(index - other), best);
      }
      refs[reverseInt(query)] = index;
    }
    return best == int.MaxValue ? -1 : best;
  }
}

public class MainClass {

  public record TC(int[] n);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([12,21,45,33,54]),
      new TC([120,21]),
      new TC([21,120]),

    ];
    foreach (var tc in testcases) {
      var result = solution.MinMirrorPairDistance(tc.n);
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

