// 1855 Maximum Distance Between a Pair of Values
// https://leetcode.com/problems/maximum-distance-between-a-pair-of-values/description/
// Difficulty: Medium
// Time Taken: 00:25:30

public class Solution {
  public int MaxDistance(int[] nums1, int[] nums2) {
    List<int> nums2ls = nums2.Reverse().ToList();
    int best = 0;
    for(int n1_index = 0; n1_index < nums1.Length; n1_index++) {
      int n2_index = nums2ls.BinarySearch(nums1[n1_index]);
      if (n2_index < 0) {
        n2_index = ~n2_index;
        n2_index = nums2.Length - n2_index - 1;
      } else {
        n2_index = nums2.Length - n2_index - 1;
        while(n2_index + 1 < nums2.Length && nums1[n1_index] == nums2[n2_index+1]) {
          n2_index += 1;
        }
      }
      if (n1_index <= n2_index) {
        best = Math.Max(n2_index - n1_index, best);  
      }
    }
    return best;
  }
}


public class MainClass {

  public record TC(int[] n, int[] m);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([55,30,5,4,2], [100,20,10,10,5]),
      new TC([2,2,2], [10,10,1]),
      new TC([30,29,19,5], [25,25,25,25,25]),

    ];
    foreach (var tc in testcases) {
      var result = solution.MaxDistance(tc.n, tc.m);
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

