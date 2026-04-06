// 874 Walking Robot Simulation
// https://leetcode.com/problems/walking-robot-simulation/description/
// Difficulty: Medium
// Time Taken: 00:20:00


public class Solution {
  public const int NORTH = 0;
  public const int EAST = 1;
  public const int SOUTH = 2;
  public const int WEST = 3;

  public int RobotSim(int[] commands, int[][] obstacles) {
    HashSet<(int, int)> obs = obstacles.Select((a) => (a[0], a[1])).ToHashSet();

    int dir = NORTH;
    int posX = 0;
    int posY = 0;
    int bestDistance = 0;
    foreach (var cmd in commands) {

      if (cmd >= 0) {
        // move direction
        // binary search for 
        for (int i = 0; i < cmd; i++) {
          if (dir == NORTH) {
            if (obs.Contains((posX, posY + 1))) {
              break;
            }
            posY += 1;
          } else if (dir == EAST) {
            if (obs.Contains((posX + 1, posY))) {
              break;
            }
            posX += 1;
          } else if (dir == WEST) {
            if (obs.Contains((posX - 1, posY))) {
              break;
            }
            posX -= 1;
          } else {
            if (obs.Contains((posX, posY - 1))) {
              break;
            }
            posY -= 1;
          }
        }
      } else if (cmd == -2) {
        // turn left 90
        dir = (dir + 4 - 1) % 4;
      } else if (cmd == -1) {
        // turn right 90
        dir = (dir + 1) % 4;
      }

      bestDistance = Math.Max(bestDistance, posX * posX + posY * posY);
    }

    return bestDistance;
  }
}

public class MainClass {

  public record TestCase(int[] e, int[][] c);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      new TestCase(new int[]{4,-1,3}, Parse2D("[]")),
      new TestCase(new int[]{4,-1,4,-2,4}, Parse2D("[[2,4]]")),
      new TestCase(new int[]{6,-1,-1,6}, Parse2D("[[0,0]]")),
    };

    foreach (var tc in testcases) {
      var result = solution.RobotSim(tc.e, tc.c);
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

