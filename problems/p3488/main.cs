// 3488 Closest Equal Element Queries
// https://leetcode.com/problems/closest-equal-element-queries/description/
// Difficulty: Medium
// Time Taken: 00:51:22

public class Solution {
  public IList<int> SolveQueries(int[] nums, int[] queries) {
    // Preprocess the nums for quicker lookups
    Dictionary<int, List<int>> refs = [];
    for(int index = 0; index < nums.Length; index++) {
      int num = nums[index];
      if (!refs.ContainsKey(num)) {
        refs[num] = [];
      }
      refs[num].Add(index);
    }


    int bestCircleIndex(int target, int query, int n) {
      int cand1 = Math.Abs(query - (target - n));
      int cand2 = Math.Abs(query - target);
      int cand3 = Math.Abs(query - (target + n));
      return Math.Min(Math.Min(cand1, cand2), cand3);
    }

    // Process the queries
    List<int> answers = [];
    foreach(var query in queries) {
      if (refs.ContainsKey(nums[query])) {
        var group = refs[nums[query]];
        if (group.Count == 1) {
          answers.Add(-1);
        } else {
          int start = group.BinarySearch(query);
          int left = (start + group.Count - 1) % group.Count;
          int right = (start + 1) % group.Count;
          int leftCand = bestCircleIndex(group[left], query, nums.Length);
          int rightCand = bestCircleIndex(group[right], query, nums.Length);
          answers.Add(Math.Min(leftCand, rightCand));
        }
      } else {
        // insert -1
        answers.Add(-1);
      }
    }

    return answers;
  }
}

public class MainClass {

  public record TC(int[] n, int[] q);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([1,3,1,4,1,3,2], [0,3,5]),
      new TC([1,2,3,4], [0,1,2,3]),
      new TC([2,10,20,20,20], [1,4,2])
    ];
    foreach (var tc in testcases) {
      var result = solution.SolveQueries(tc.n, tc.q);
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


