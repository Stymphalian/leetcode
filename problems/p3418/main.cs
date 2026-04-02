// 3418 Maximum Amount of Money Robot Can Earn
// https://leetcode.com/problems/maximum-amount-of-money-robot-can-earn/description/
// Difficulty: Medium
// Time Taken: Failed

public class Solution {

  public int Solve(int[][] coins, int x, int y, int k, Dictionary<(int, int, int), int> memo) {
    if (y >= coins.Length) { return int.MinValue; }
    if (x >= coins[0].Length) { return int.MinValue; }
    if (y == coins.Length - 1 && x == coins[0].Length-1) {
      if (k > 0) {
        return Math.Max(coins[y][x], 0);
      } else {
        return coins[y][x];
      }
    }

    var key = (x, y, k);
    if (memo.ContainsKey(key)) {
      return memo[key];
    }

    // take the coin
    int rightTake = Solve(coins, x+1, y, k, memo);
    int bottomTake = Solve(coins, x, y+1, k, memo);
    int best = Math.Max(rightTake, bottomTake) + coins[y][x];
    if (k > 0 && coins[y][x] < 0) {
      int rightNoTake = Solve(coins, x+1, y, k-1, memo);
      int bottomNoTake = Solve(coins, x, y+1, k-1, memo);
      best = Math.Max(best, Math.Max(rightNoTake, bottomNoTake));
    }

    memo[key] = best;
    return best;
  }

  public int MaximumAmount(int[][] coins) {
    return Solve(coins, 0, 0, 2, []);
  }
}


public class MainClass {

  public record TestCase(int[][] c);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      new TestCase(Parse2D("[[0,1,-1],[1,-2,3],[2,-3,4]]")),
      new TestCase(Parse2D("[[10,10,10],[10,10,10]]")),
      new TestCase(Parse2D("[[-7,12,12,13],[-6,19,19,-6],[9,-2,-10,16],[-4,14,-10,-9]]")), // 60
      new TestCase(Parse2D("[[-16,8,-7,-19],[6,3,-10,13],[13,15,4,-3],[-16,4,19,-12]]")), // 57
      new TestCase(Parse2D("[[-18,-12,-13,-11,17],[11,-7,-9,5,-8],[-3,5,-16,-18,9],[-7,-17,-5,3,-5],[12,-3,4,15,-7]]")), // 29
      new TestCase(Parse2D("[[10,-17,6,-10],[-3,0,-2,-20],[-4,-6,15,-7],[13,-7,-8,-13]]")), // 22
      new TestCase(Parse2D("[[-16,4,1,-1],[11,9,3,3],[-6,17,-19,9],[14,-17,-19,-13]]")) // 35
    };

    foreach (var tc in testcases) {
      var result = solution.MaximumAmount(tc.c);
      Console.WriteLine(result);
    }
  }


  public static int[] Parse1D(string input) {
    var array = Array.ConvertAll(input.Trim('[', ']').Split(','), int.Parse);
    return array;
  }

  public static int[][] Parse2D(string input) {
    var rows = input.Trim('[', ']').Split("],[");
    var array = new int[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
      array[i] = Array.ConvertAll(rows[i].Split(','), int.Parse);
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


