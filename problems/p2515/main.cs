// 2515 Shortest Distance to Target String in a Circular Array
// https://leetcode.com/problems/shortest-distance-to-target-string-in-a-circular-array/description/
// Difficulty: Easy
// Time Taken: 00:15:35


public class Solution {
  public int ClosestTarget(string[] words, string target, int startIndex) {

    int best = int.MaxValue;
    for(int index = 0; index < words.Length; index++) {
      if (words[index] == target) {
        int cand1 = Math.Abs(startIndex - index);
        int cand2 = Math.Abs(startIndex - (index+words.Length));
        int cand3 = Math.Abs(startIndex - (index-words.Length));
        best = Math.Min(best, Math.Min(Math.Min(cand1, cand2), cand3));
      }
    }
    if (best == int.MaxValue) {
      return -1;
    } else {
      return best;
    }
  }
}

public class MainClass {

  public record TC(string[] w, string t, int s);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC(["hello","i","am","leetcode","hello"], "hello", 1),
      new TC(["a","b","leetcode"], "leetcode", 0),
      new TC(["i","eat","leetcode"], "ate", 0),
      new TC(["hsdqinnoha","mqhskgeqzr","zemkwvqrww","zemkwvqrww","daljcrktje","fghofclnwp","djwdworyka","cxfpybanhd","fghofclnwp","fghofclnwp"], "zemkwvqrww", 8)
    ];
    foreach (var tc in testcases) {
      var result = solution.ClosestTarget(tc.w, tc.t, tc.s);
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

