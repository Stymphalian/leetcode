// 2751 Robot Collisions
// https://leetcode.com/problems/robot-collisions/description/
// Difficulty: Hard
// Time Taken: 01:19:22

using System.Data;

public class Solution {

  public const int RIGHT = 1;
  public const int LEFT = -1;

  public class Robot {
    public int Pos;
    public int Health;
    public int Dir;
    public int Ordinal;
    public bool Alive;
    public Robot(int pos, int health, int dir, int ordinal) {
      Pos = pos;
      Health = health;
      Dir = dir;
      Ordinal = ordinal;
      Alive = true;
    }
  }

  public IList<int> SurvivedRobotsHealths(int[] positions, int[] healths, string directions) {
    int n = positions.Length;
    List<Robot> robots = [];
    for (int index = 0; index < n; index++) {
      var robot = new Robot(positions[index], healths[index], directions[index] == 'R' ? 1 : -1, index);
      robots.Add(robot);
    }

    // Sort the robots based on their position
    robots = robots.OrderBy(a => a.Pos).ToList();

    List<Robot> resolveCollisions(List<Robot> rights, List<Robot> lefts) {
      while (rights.Count > 0 && lefts.Count > 0) {
        var right = rights[rights.Count - 1];
        var left = lefts[0];
        if (right.Health > left.Health) {
          right.Health -= 1;
          lefts.RemoveAt(0);
        } else if (right.Health < left.Health) {
          rights.RemoveAt(rights.Count - 1);
          left.Health -= 1;
        } else {
          rights.RemoveAt(rights.Count - 1);
          lefts.RemoveAt(0);
        }
      }
      return [.. rights, .. lefts];
    }


    while (true) {
      List<Robot> answer = [];
      List<Robot> rights = [];
      List<Robot> lefts = [];
      int state = 0;

      for (int index = 0; index < robots.Count; index++) {
        var current = robots[index];
        if (state == 0) {
          if (current.Dir == LEFT) {
            answer.Add(current);
          } else {
            state = 1;
            rights.Add(current);
          }
        } else if (state == 1) {
          if (current.Dir == RIGHT) {
            // Add to list
            rights.Add(current);
          } else {
            state = 2;
            lefts.Add(current);
          }
        } else if (state == 2) {
          if (current.Dir == LEFT) {
            // Add to list
            lefts.Add(current);
          } else {
            var outs = resolveCollisions(rights, lefts);
            foreach (var r in outs) {
              answer.Add(r);
            }

            rights.Clear();
            lefts.Clear();
            rights.Add(current);
            state = 1;
          }
        }
      }

      if (rights.Count > 0 || lefts.Count > 0) {
        var outs = resolveCollisions(rights, lefts);
        foreach (var r in outs) {
          answer.Add(r);
        }
      }

      if (answer.Count == robots.Count) {
        break;
      }
      robots = answer;
    }

    return robots.OrderBy(a => a.Ordinal).Select(a => a.Health).ToList();
  }
}


public class MainClass {

  public record TestCase(int[] p, int[] h, string d);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      new TestCase(new int[] {5,4,3,2,1}, new int[] {2,17,9,15,10}, "RRRRR"),
      new TestCase(new int[] {3,5,2,6}, new int[] {10, 10, 15, 12}, "RLRL"),
      new TestCase(new int[] {1,2,5,6}, new int[] {10, 10, 11, 11}, "RLRL"),
      new TestCase(new int[] {1,40}, new int[] {10,11}, "RL"),
      new TestCase(new int[]{30,41,35,18}, new int[] {13, 33, 9, 25}, "LLRR"),
    };

    foreach (var tc in testcases) {
      var result = solution.SurvivedRobotsHealths(tc.p, tc.h, tc.d);
      PrintArray(result);
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

