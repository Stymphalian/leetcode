// 2452 Words Within Two Edits of Dictionary
// https://leetcode.com/problems/words-within-two-edits-of-dictionary/description/
// Difficulty: Medium
// Time Taken: 00:07:42

public class Solution {
  public IList<string> TwoEditWords(string[] queries, string[] dictionary) {
    List<string> answers = [];

    int distance(string q, string d) {
      int dist = 0;
      for(int index = 0; index < q.Length; index++) {
        if (q[index] != d[index]) {dist += 1;}
      }
      return dist;
    }
    foreach(var query in queries) {
      foreach(var dict in dictionary) {
        int dist = distance(query, dict);
        if (dist <= 2) {
          answers.Add(query);
          break;
        }
      }
    }
    return answers;
  }
}

public class MainClass {

  public record TC(string[] a, string[] b);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC(["word","note","ants","wood"], ["wood","joke","moat"]),
      new TC(["yes"], ["not"]),
    ];
    foreach (var tc in testcases) {
      var result = solution.TwoEditWords(tc.a, tc.b);
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

