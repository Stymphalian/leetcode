// 3661 Maximum Walls Destroyed by Robots
// https://leetcode.com/problems/maximum-walls-destroyed-by-robots/description/
// Difficulty: Hard
// Time Taken: 03:02:37

// fuck my brain.
public class Solution {
  public const int LEFT = 0;
  public const int RIGHT = 1;
  public record RobotWall(int Pos, int Dist);
  public record Range(int left, int right);

  public int MaxWalls(int[] robots, int[] distance, int[] walls) {
    int n = robots.Length;
    List<RobotWall> rw = [];
    for (int index = 0; index < n; index++) {
      rw.Add(new RobotWall(robots[index], distance[index]));
    }
    rw = rw.OrderBy(x => x.Pos).ToList();
    walls = walls.ToList().Order().ToArray();


    (Range, Range) getRange(int index) {
      var current = rw[index];
      var leftDist = current.Pos - current.Dist;
      var rightDist = current.Pos + current.Dist;
      int leftBound = leftDist;
      if (index - 1 >= 0 && rw[index-1].Pos >= leftDist) {
        leftBound = rw[index-1].Pos + 1;
      }
      int rightBound = rightDist;
      if (index + 1 < n && rw[index+1].Pos <= rightDist) {
        rightBound = rw[index+1].Pos - 1;
      }

      Range left = new(leftBound, current.Pos);
      Range right = new(current.Pos, rightBound);
      return (left, right);
    }

    int countWallsInRange(Range range) {
      int leftIndex = Array.BinarySearch(walls, range.left);
      int rightIndex = Array.BinarySearch(walls, range.right);
      if (leftIndex < 0) { leftIndex = ~leftIndex; }
      if (rightIndex < 0) { rightIndex = ~rightIndex; }
      else { rightIndex++; } // include right endpoint (always inclusive now)
      return rightIndex - leftIndex;
    }

    var (prevRangeLeft, prevRangeRight) = getRange(0);
    int prevCountLeft = countWallsInRange(prevRangeLeft);
    int prevCountRight = countWallsInRange(prevRangeRight);
    int[] dp = [prevCountLeft, prevCountRight];
    for (int index = 1; index < n; index++) {
      var (currentLeft, currentRight) = getRange(index);
      var countLeft = countWallsInRange(currentLeft);
      var countRight = countWallsInRange(currentRight);
      var countBetweenRobots = countWallsInRange(new(rw[index-1].Pos, rw[index].Pos));

      int oldLeft = dp[LEFT];
      int oldRight = dp[RIGHT];
      // Assume we shoot left
      dp[LEFT] = Math.Max(
        oldLeft + countLeft,
        oldRight - prevCountRight + Math.Min(prevCountRight + countLeft, countBetweenRobots)
      );
      // Assume we shoot right
      dp[RIGHT] = Math.Max(
        oldLeft + countRight,
        oldRight + countRight
      );
      prevRangeLeft = currentLeft;
      prevRangeRight = currentRight;
      prevCountLeft = countLeft;
      prevCountRight = countRight;
    }

    return Math.Max(dp[0], dp[1]);
  }
}


public class MainClass {

  public record TestCase(int[] r, int[] d, int[] w);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      new TestCase([4],[3], [1,10]), // 1
      new TestCase([10, 2], [5,1], [5,2,7]), // 3
      new TestCase([1,2], [100,1], [10]), // 0
      new TestCase(
        [17,59,32,11,72,18],
        [5,7,6,5,2,10],
        [17,25,33,29,54,53,18,35,39,37,20,14,34,13,16,58,22,51,56,27,10,15,12,23,45,43,21,2,42,7,32,40,8,9,1,5,55,30,38,4,3,31,36,41,57,28,11,49,26,19,50,52,6,47,46,44,24,48]
      ), // 37
    };

    foreach (var tc in testcases) {
      var result = solution.MaxWalls(tc.r, tc.d, tc.w);
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

