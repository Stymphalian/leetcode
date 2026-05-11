// 2553 Separate the Digits in an Array
// https://leetcode.com/problems/separate-the-digits-in-an-array/description/
// Difficulty: Easy
// Time Taken: 00:05:06

public class Solution {
  public int[] SeparateDigits(int[] nums) {
    List<int> answer = [];
    foreach(var num in nums) {

      // parse out the digits
      List<int> digits = [];
      int current = num;
      while(current > 0) {
        int digit = current % 10;
        digits.Add(digit);
        current /= 10;
      }
      if (current > 0) {
        digits.Add(current);
      }

      digits.Reverse();
      foreach(var digit in digits) {
        answer.Add(digit);
      }
    }

    return answer.ToArray();
  }
}

public class MainClass {

  public record TC(int[] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([13,25, 83, 77]),
      new TC([7,1,3,9]),
    ];
    foreach (var tc in testcases) {
      var result = solution.SeparateDigits(tc.a);
      PrintArray(result);
      // Console.WriteLine(result);
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

