// 3783 Mirror Distance of an Integer
// https://leetcode.com/problems/mirror-distance-of-an-integer/description/
// Difficulty: Easy
// Time Taken: 00:02:03

public class Solution {
  public int MirrorDistance(int n) {
    int reverse = 0;
    int n2 = n;
    while (n2 > 0) {
      int digit = n2 % 10;
      reverse *= 10;
      reverse += digit;
      n2 /= 10;
    }
    reverse += n2;

    int dist = Math.Abs(n - reverse);
    return dist;
  }
}

public class MainClass {

  public record TC(int n);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC(25),
      new TC(10),
      new TC(7),

    ];
    foreach (var tc in testcases) {
      var result = solution.MirrorDistance(tc.n);
      // PrintArray(result);
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

