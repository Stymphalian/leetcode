// 1189 Maximum Number of Balloons
// https://leetcode.com/problems/maximum-number-of-balloons/description/
// Difficulty: Easy
// Time Taken: 00:03:48


public class Solution {
  public int MaxNumberOfBalloons(string text) {
    int b = 0;
    int a = 0;
    int l = 0;
    int o = 0;
    int n = 0;
    foreach(var c in text) {
      if (c == 'b') {
        b += 1;
      } else if (c == 'a') {
        a += 1;
      } else if (c == 'l') {
        l += 1;
      } else if (c == 'o') {
        o += 1;        
      } else if (c == 'n') {
        n += 1;
      }
    }

    l /= 2;
    o /= 2;
    int count = Math.Min(b, a);
    count = Math.Min(count, l);
    count = Math.Min(count, o);
    count = Math.Min(count, n);
    return count;
  }
}

public class MainClass {

  public record TC(string a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC("nlaebolko"),
      new TC("loonbalxballpoon"),
      new TC("leetcode"),
    ];
    foreach (var tc in testcases) {
      var result = solution.MaxNumberOfBalloons(tc.a);
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

