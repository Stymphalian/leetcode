// 2833 Furthest Point From Origin
// https://leetcode.com/problems/furthest-point-from-origin/description/
// Difficulty: Easy
// Time Taken: 00:05:03

public class Solution {
  public int FurthestDistanceFromOrigin(string moves) {
    int countLeft = 0;
    int countRight = 0;
    int countSpare = 0;
    foreach(var move in moves) {
      if (move == '_') {
        countSpare += 1;
      } else if (move == 'L') {
        countLeft += 1;
      } else {
        countRight += 1;
      }
    }
    return Math.Max((
      Math.Abs(countRight + countSpare) - countLeft),
      Math.Abs(countRight - (countLeft + countSpare))
    );
  }
}


public class MainClass {

  public record TC(string m);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC("L_RL__R"),
      new TC("_R__LL_"),
      new TC("_______"),
    ];
    foreach (var tc in testcases) {
      var result = solution.FurthestDistanceFromOrigin(tc.m);
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

