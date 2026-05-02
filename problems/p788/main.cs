// 788 Rotated Digits
// https://leetcode.com/problems/rotated-digits/description/
// Difficulty: Medium
// Time Taken: 00:26:30


public class Solution {

  int[] rotations = [0, 1, 5, -1, -1, 2, 9, -1, 8, 6];

  public bool IsGood(int num) {
    int cand = num;
    int next = 0;
    int multiplier = 1;
    while(cand > 0) {
      int digit = cand % 10;
      cand /= 10;

      int rdigit = rotations[digit];
      if (rdigit == -1) {return false;}

      next += rdigit*multiplier;
      multiplier *= 10;
    }
    next += cand*multiplier;
    // Console.WriteLine($"{num}, {next}");
    if (next == num) {return false;}
    
    return true;
  }

  public int RotatedDigits(int n) {
    int count = 0;
    for(int index = 1; index <= n; index++) {
      if (IsGood(index)) {
        count += 1;     
      }
    }
    return count;
  }
}

public class MainClass {

  public record TC(int a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC(10),
      new TC(1),
      new TC(2)
    ];
    foreach (var tc in testcases) {
      var result = solution.RotatedDigits(tc.a);
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

