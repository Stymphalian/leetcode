// 3612 Process String with Special Operations I
// https://leetcode.com/problems/process-string-with-special-operations-i/description/
// Difficulty: Medium
// Time Taken: 00:10:13

using System.Text;

public class Solution {
  public string ProcessStr(string s) {
    List<char> result = [];

    foreach(var op in s) {
      if (op == '#') {
        // duplicate
        int n = result.Count;
        for(int idx = 0; idx < n; idx++) {
          result.Add(result[idx]);
        }
      } else if (op == '%') {
        // reverse
        int left = 0;
        int right = result.Count -1;
        while(left < right) {
          char temp = result[left];
          result[left] = result[right];
          result[right] = temp;
          left++;
          right--;
        }
      } else if (op == '*') {
        // remove
        if (result.Count > 0) {
          result.RemoveAt(result.Count-1);  
        }
      } else {
        // append
        result.Add(op);
      }
    }

    StringBuilder b = new();
    foreach(var c in result) {
      b.Append(c);
    }
    return b.ToString();
  }
}


public class MainClass {

  public record TC(string a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC("a#b%*"),
      new TC("z*#"),
    ];
    foreach (var tc in testcases) {
      var result = solution.ProcessStr(tc.a);
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

