// 3614 Process String with Special Operations II
// https://leetcode.com/problems/process-string-with-special-operations-ii/description/
// Difficulty: Hard
// Time Taken: 01:17:03


public class Solution {
  public char ProcessStr(string s, long k) {
    long totalSize = 0;
    foreach(var c in s) {
      if (c == '#') {
        totalSize *= 2;
      } else if (c == '%') {
        // pass
      } else if (c == '*') {
        if (totalSize > 0) {
          totalSize -= 1;  
        }
      } else {
        totalSize += 1;
      }
    }
    if (k >= totalSize) {return '.';}

    long n = totalSize;
    long target = k;
    for(int idx = s.Length-1; idx >= 0; idx--) {
      char op = s[idx];
      if (op == '#') {
        if (target >= n/2) {
          target -= n/2;
        }
        n = n/2;
      } else if (op == '%') {
        target = n - target - 1;
      } else if (op == '*') {
        n += 1;
      } else {
        if (target == n-1) {
          return op;
        }
        n -= 1;
      }
    }

    return '.';
  }
}

public class MainClass {

  public record TC(string a, int b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      // new TC("a#b%*", 1),
      // new TC("cd%#*#", 3),
      // new TC("z*#", 0),
      new TC("%#*gm#xib", 2)
    ];
    foreach (var tc in testcases) {
      var result = solution.ProcessStr(tc.a, tc.b);
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

