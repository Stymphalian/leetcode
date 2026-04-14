// 2463 Minimum Total Distance Traveled
// https://leetcode.com/problems/minimum-total-distance-traveled/description/
// Difficulty: Hard
// Time Taken: 01:46:20

// Failed

public class Solution {
  public const int ROBOT = 0;
  public const int FACTORY = 1;
  public record Thing(int Type, int Pos, int Limit);

  public (bool, long) DP(int[] robots, int[] factories, int ri, int fi, Dictionary<(int,int),(bool, long)> memo) {
    var key = (ri, fi);
    if (memo.ContainsKey(key)) {
      return memo[key];
    }
    if (ri >= robots.Length) {
      return (true, 0);
    }
    if (fi >= factories.Length) {
      if (ri < robots.Length) {
        return (false, 0);
      } else {
        throw new Exception("IMPOSSIBLE STATE");
      }
    }

    // use or skip
    int robot_pos = robots[ri];
    var (ok1, dist1) = DP(robots, factories, ri+1, fi+1, memo);
    var (ok2, dist2) = DP(robots, factories, ri, fi+1, memo);
    if (!ok1 && !ok2) {
      memo[key] = (false, 0);
    } else if (!ok1) {
      memo[key] = (true, dist2);
    } else if (!ok2) {
      memo[key] = (true, dist1 + Math.Abs(robot_pos-factories[fi]));
    } else {
      memo[key] = (true, Math.Min(dist1 + Math.Abs(robot_pos-factories[fi]), dist2));
    }
    return memo[key];
  }

  public long MinimumTotalDistance(IList<int> robot, int[][] factory) {
    int[] robots = robot.OrderBy(x => x).ToArray();
    int[] factories = factory
      .Select(x => {
        int[] p = new int[x[1]];
        Array.Fill(p, x[0]);
        return p;
      })
      .SelectMany(x => x)
      .Order()
      .ToArray();

    return DP(robots, factories, 0, 0, []).Item2;
  }
}

public class MainClass {

  public record TC(int[] e, int[][] f);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([0,4,6], Parse2D("[[2,2],[6,2]]")),
      new TC([1,-1], Parse2D("[[-2,1],[2,1]]")),
      new TC([9,11, 99,101], Parse2D("[[10,1],[7,1],[14,1],[100,1],[96,1],[103,1]]")),
      new TC(
        [-333539942,359275673,89966494,949684497,-733065249,241002388,325009248,403868412,-390719486,-670541382,563735045,119743141,323190444,534058139,-684109467,425503766,761908175], 
        Parse2D("[[-590277115,0],[-80676932,3],[396659814,0],[480747884,9],[118956496,10]]")
      )
    ];
    foreach (var tc in testcases) {
      var result = solution.MinimumTotalDistance(tc.e, tc.f);
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

