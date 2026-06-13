// 3838 Weighted Word Mapping
// https://leetcode.com/problems/weighted-word-mapping/description/
// Difficulty: Easy
// Time Taken: 00:07:45

using System.Text;

public class Solution {
  public string MapWordWeights(string[] words, int[] weights) {
    StringBuilder answer = new();
    foreach(var word in words) {
      int next = 0;
      foreach(var c in word) {
        int ci = c - 'a';
        int w = weights[ci];
        next  = (next + w) % 26;
      }

      int reverse_next = (26 - next - 1);
      int cj = (int)'a' + reverse_next;
      char letter = (char) cj;
      answer.Append(letter);
    }

    return answer.ToString();
  }
}


public class MainClass {

  public record TC(string[] a, int[] b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      // new TC(Parse2D("[[1,2]"), Parse2D("[[1,1],[1,2]]")),
      new TC(
        ["abcd","def","xyz"],
        [5,3,12,14,1,2,3,2,10,6,6,9,7,8,7,10,8,9,6,9,9,8,3,7,7,2]
      ),
      new TC(
        ["a","b","c"],
        [1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1]
      ),
      new TC(
        ["abcd"],
        [7,5,3,4,3,5,4,9,4,2,2,7,10,2,5,10,6,1,2,2,4,1,3,4,4,5]
      ),
    ];
    foreach (var tc in testcases) {
      var result = solution.MapWordWeights(tc.a, tc.b);
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

  // [["#",".","*","."],["#","#","*","."]]
  public static char[][] ParseChar2D(string input) {
    input = input.Trim();
    var inner = input.Substring(1, input.Length - 2).Trim();
    if (inner == "") return new char[0][];
    var rows = inner.Split("],[");
    var array = new char[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
      var row = rows[i].Trim('[', ']').Trim();
      array[i] = row == "" ? new char[0] : row.Split(',').Select(s => s.Trim('"', ' ')[0]).ToArray();
    }
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

