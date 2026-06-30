// 1358 Number of Substrings Containing All Three Characters
// https://leetcode.com/problems/number-of-substrings-containing-all-three-characters/description/
// Difficulty: Medium
// Time Taken: 00:21:02

// cheat

public class Solution {
  public int NumberOfSubstrings(string s) {
    int[] last = new int[3] {-1,-1,-1};
    int answer = 0;
    for(int idx = 0; idx < s.Length; idx++) {
      int ci = s[idx] - 'a';
      last[ci] = idx;

      if (last[0] >= 0 && last[1] >= 0 && last[2] >= 0) {
        answer += 1 + Math.Min(Math.Min(last[0], last[1]), last[2]);
      }
    }

    return answer;
  }
}



public class MainClass {

  public record TC(string a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC("abcabc"),
      new TC("aaacb"),
      new TC("abc"),
    ];
    foreach (var tc in testcases) {
      var result = solution.NumberOfSubstrings(tc.a);
      // PrintListNode(result);
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

