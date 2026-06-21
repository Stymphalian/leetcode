// 1833 Maximum Ice Cream Bars
// https://leetcode.com/problems/maximum-ice-cream-bars/description/
// Difficulty: Medium
// Time Taken: 00:20:23

public class Solution {

  public const int LIMIT = 100001;
  public int MaxIceCream(int[] costs, int coins) {
    int[] radix = new int[LIMIT];

    for(int idx = 0; idx < costs.Length; idx++) {
      radix[costs[idx]] += 1;
    }

    int answer = 0;
    for(int cost = 1; cost < LIMIT; cost++) {
      if (cost > coins) {break;}

      int n = radix[cost];
      int m = coins / cost;
      int p = Math.Min(n,m);

      answer += p;
      coins -= p * cost;
    }

    return answer;
  }
}

public class MainClass {

  public record TC(int[] a, int b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([1,3,2,4,1], 7),
      new TC([10,6,8,7,7,8], 5),
      new TC([1,6,3,1,2,5], 20),
    ];
    foreach (var tc in testcases) {
      var result = solution.MaxIceCream(tc.a, tc.b);
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

