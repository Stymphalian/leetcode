// 2615 Sum of Distances
// https://leetcode.com/problems/sum-of-distances/description/
// Difficulty: Medium
// Time Taken: 01:32:28

// Failed
// Learned - use formulas to see the pattern

public class Solution {
  public long[] Distance(int[] nums) {
    long[] answer = new long[nums.Length];
    Array.Fill(answer, 0);

    Dictionary<int, List<int>> groups = [];
    for(int index1 = 0; index1 < nums.Length; index1++) {
      int val = nums[index1];
      if (!groups.ContainsKey(val)) {
        groups[val] = [];
      }
      groups[val].Add(index1);
    }

    foreach(var (val, group) in groups) {
      long[] prefix = new long[group.Count];
      prefix[0] = group[0];
      if (group.Count == 1) {
        // answer[group[0]] = 0;
        continue;        
      }

      for(int index = 1; index < group.Count; index++) {
        prefix[index] = prefix[index-1] + (long) group[index];
      }

      for(int index = 0; index < group.Count; index++) {
        long left_count = index;
        long right_count = group.Count - index - 1;
        long left_sum = (index - 1 >= 0 ) ? prefix[index-1] : 0;
        long right_sum = prefix[group.Count-1] - prefix[index];
        long target = group[index];

        long left = left_count*target - left_sum;
        long right = right_sum - right_count*target;
        answer[target] = left + right;
      }
    }

    return answer;
  }
}

public class MainClass {

  public record TC(int[] a);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([1,3,1,1,2]),
      new TC([0,5,3]),
      new TC([2,1,3,1,2,3,3])
    ];
    foreach (var tc in testcases) {
      var result = solution.Distance(tc.a);
      PrintArray(result);
      // Console.WriteLine(result);
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

