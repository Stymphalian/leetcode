// 2946 Matrix Similarity After Cyclic Shifts
// https://leetcode.com/problems/matrix-similarity-after-cyclic-shifts/description/
// Difficulty: Easy
// Time Taken: 00:18:20

public class Solution {
  public bool AreSimilar(int[][] mat, int k) {
    int height = mat.Length;
    int width = mat[0].Length;
    k = k % width;

    for(int y = 0; y < height; y++) {

      int original = 17;
      for(int x = 0; x < width; x++) {
        original = original * 23 + mat[y][x].GetHashCode();
      }

      int hash = 17;
      if (y % 2 == 0) {
        for(int x = 0; x < width; x++) {
          hash = hash * 23 + mat[y][(x+k) % width].GetHashCode();
        }  
      } else {
        for(int x = 0; x < width; x++) {
          hash = hash * 23 + mat[y][(x+width-k) % width].GetHashCode();
        }  
      }

      if (original != hash) {
        return false;
      }
    }

    return true;
  }
}

public class MainClass {

  public record TestCase(int[][] mat, int k);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      new TestCase(ParseArray2D("[[1,2,3],[4,5,6],[7,8,9]]"), 4), // true
      new TestCase(ParseArray2D("[[1,2,1,2],[5,5,5,5],[6,3,6,3]]"), 2), // true
      new TestCase(ParseArray2D("[[2,2],[2,2]]"), 3), // true
    };

    foreach (var tc in testcases) {
      var result = solution.AreSimilar(tc.mat, tc.k);
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

