// 1886 Determine Whether Matrix Can Be Obtained By Rotation
// https://leetcode.com/problems/determine-whether-matrix-can-be-obtained-by-rotation/description/
// Difficulty: Easy
// Time Taken: 00:22:53

public class Solution {
  public bool FindRotation(int[][] mat, int[][] target) {
    // if (hash(mat) != hash(target)) { return false; }
    bool rot_0 = true;
    bool rot_90 = true;
    bool rot_180 = true;
    bool rot_270 = true;
    int height = mat.Length;
    int width = mat[0].Length;
    int hash1 = 0;
    int hash2 = 0;
    for (int y = 0; y < height; y++) {
      for (int x = 0; x < width; x++) {
        if (rot_0) {
          rot_0 &= mat[y][x] == target[y][x];
        }
        if (rot_90) {
          rot_90 &= mat[y][x] == target[x][width-y-1];
        }
        if (rot_180) {
          rot_180 &= mat[y][x] == target[height-y-1][width-x-1];
        }
        if (rot_270) {
          rot_270 &= mat[y][x] == target[width-x-1][y];
        }
        hash1 += mat[y][x];
        hash2 += target[y][x];
      }
    }

    return (rot_0 || rot_90 || rot_180 || rot_270) && (hash1 == hash2);
  }
}

public class MainClass {

  public record TestCase(int[][] mat1, int[][] mat2);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      // [[0,1],[1,0]], [[1,0],[0,1]]
      new TestCase(
        ParseArray2D("[[0,1],[1,0]]"),
        ParseArray2D("[[1,0],[0,1]]")
      ),
      // [[0,1],[1,1]], [[1,0],[0,1]]
      new TestCase(
        ParseArray2D("[[0,1],[1,1]]"),
        ParseArray2D("[[1,0],[0,1]]")
      ),
      // [[0,0,0],[0,1,0],[1,1,1]], [[1,1,1],[0,1,0],[0,0,0]]
      new TestCase(
        ParseArray2D("[[0,0,0],[0,1,0],[1,1,1]]"),
        ParseArray2D("[[1,1,1],[0,1,0],[0,0,0]]")
      ),
      // [[0,0,0],[0,0,1],[0,0,1]], [[0,0,0],[0,0,1],[0,0,1]]
      new TestCase(
        ParseArray2D("[[0,0,0],[0,0,1],[0,0,1]]"),
        ParseArray2D("[[0,0,0],[0,0,1],[0,0,1]]")
      )
    };

    foreach (var tc in testcases) {
      var result = solution.FindRotation(tc.mat1, tc.mat2);
      Console.WriteLine(result);
    }
  }


  public static int[] ParseArray(string input) {
    var array = Array.ConvertAll(input.Trim('[', ']').Split(','), int.Parse);
    return array;
  }

  public static int[][] ParseArray2D(string input) {
    var rows = input.Trim('[', ']').Split("],[");
    var array = new int[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
      array[i] = Array.ConvertAll(rows[i].Split(','), int.Parse);
    }
    return array;
  }

  public static void PrintArray<T>(T[] array, string prefix = "") {
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

