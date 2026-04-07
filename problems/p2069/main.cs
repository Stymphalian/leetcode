// 2069 Walking Robot Simulation II
// https://leetcode.com/problems/walking-robot-simulation-ii/description/
// Difficulty: Medium
// Time Taken: 00:37:52

public class Robot {

  public const int NORTH = 0;
  public const int EAST = 1;
  public const int SOUTH = 2;
  public const int WEST = 3;
  public const int X = 0;
  public const int Y = 1;
  public int dir = EAST;
  public int[] pos;
  public int width;
  public int height;

  public Robot(int width, int height) {
    this.width = width;
    this.height = height;
    this.pos = new int[2];
    this.pos[X] = 0;
    this.pos[Y] = 0;
  }

  private void turn90CCW() {
    dir = (dir + 4 - 1) % 4;
  }
  private void turn90CW() {
    dir = (dir + 1) % 4;
  }

  public void Step(int num) {
    int perm = (width-1)*2 + (height-1)*2;
    if (num >= perm)  {
      if (pos[X] == 0 && pos[Y] == 0) {
        dir = SOUTH;
      } else if (pos[X] == width-1 &&  pos[Y] == 0) {
        dir = EAST;
      } else if (pos[X] == width-1 && pos[Y] == height-1) {
        dir = NORTH;
      } else if (pos[X] == 0 && pos[Y] == height-1) {
        dir = WEST;
      }
    }
    num = num % perm;

    for (int step = 0; step < num; step++) {
      if (dir == NORTH) {
        if (pos[Y] + 1 >= height) {
          step -= 1;
          turn90CCW();
        } else {
          pos[Y] += 1;
        }

      } else if (dir == EAST) {
        if (pos[X] + 1 >= width) {
          step -= 1;
          turn90CCW();
        } else {
          pos[X] += 1;
        }

      } else if (dir == SOUTH) {
        if (pos[Y] - 1 < 0) {
          step -= 1;
          turn90CCW();
        } else {
          pos[Y] -= 1;
        }

      } else if (dir == WEST) {
        if (pos[X] - 1 < 0) {
          step -= 1;
          turn90CCW();
        } else {
          pos[X] -= 1;
        }
      }
    }
  }

  public int[] GetPos() {
    return pos;
  }

  public string GetDir() {
    if (dir == NORTH) {
      return "North";
    } else if (dir == EAST) {
      return "East";
    } else if (dir == SOUTH) {
      return "South";
    } else {
      return "West";
    }
  }
}

public class MainClass {

  public record TestCase(int[] e, int[][] c);

  public static void Simulate(string[] commands, int[][] args_list) {
    Robot? robot = null;
    for (int i = 0; i < commands.Length; i++) {
      var cmd = commands[i];
      var a = args_list[i];
      switch (cmd) {
        case "Robot":
          robot = new Robot(a[0], a[1]);
          Console.WriteLine("Robot");
          break;
        case "step":
          robot!.Step(a[0]);
          Console.WriteLine($"step {a[0]}");
          break;
        case "getPos":
          PrintArray(robot!.GetPos());
          break;
        case "getDir":
          Console.WriteLine(robot!.GetDir());
          break;
      }
    }
  }

  public static void Main(string[] args) {
    var commands = new string[] { "Robot", "step", "step", "getPos", "getDir", "step", "step", "step", "getPos", "getDir" };
    var args_list = new int[][] { new[] { 6, 3 }, new[] { 2 }, new[] { 2 }, new int[0], new int[0], new[] { 2 }, new[] { 1 }, new[] { 4 }, new int[0], new int[0] };
    Simulate(commands, args_list);
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

