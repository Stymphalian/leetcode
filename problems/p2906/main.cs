// 2906 Construct Product Matrix
// https://leetcode.com/problems/construct-product-matrix/description/
// Difficulty: Medium
// Time Taken: 01:05:28

public class Solution {

  public int[][] ConstructProductMatrix(int[][] grid) {
    int height = grid.Length;
    int width = grid[0].Length;

    const long MOD = 12345;
    int[][] answer = new int[height][];
    long[][] topToBot = new long[height][];
    long[][] botToTop = new long[height][];
    long[][] leftToRight = new long[height][];
    long[][] rightToLeft = new long[height][];
    for (int y = 0; y < height; y++) {
      answer[y] = new int[width];
      topToBot[y] = new long[width];
      botToTop[y] = new long[width];
      leftToRight[y] = new long[width];
      rightToLeft[y] = new long[width];
    }

    for (int y = 0; y < height; y++) {
      for(int x = 0; x < width; x++) {
        long val = (x-1 >= 0) ? leftToRight[y][x-1] : 1;
        leftToRight[y][x] = val * grid[y][x] % MOD;
      }
      for(int x = width-1; x >= 0; x--) {
        long val = (x+1 < width) ? rightToLeft[y][x+1] : 1;
        rightToLeft[y][x] = val * grid[y][x] % MOD;
      }
      for(int x = 0; x< width; x++) {
        long upper = (y-1 >= 0) ? topToBot[y-1][x] : 1;
        topToBot[y][x] = upper * leftToRight[y][x] % MOD;
      }
    }
    for(int y = height-1; y >= 0; y--) {
      for(int x = 0; x < width; x++) {
        long lower = (y+1 < height) ? botToTop[y+1][x] : 1;
        botToTop[y][x] = lower * leftToRight[y][x] % MOD;
      }
    }

    for (int y = 0; y < height; y++) {
      for(int x = 0; x < width; x++) {
        long top = (y-1 >= 0) ? topToBot[y-1][width-1] : 1;
        long bot = (y+1 < height) ? botToTop[y+1][width-1] : 1;
        long left = (x-1 >= 0) ? leftToRight[y][x-1] : 1;
        long right = (x+1 < width) ? rightToLeft[y][x+1] : 1;
        long temp1 = top * bot % MOD;
        long temp2 = left * right % MOD;
        answer[y][x] = (int) (temp1 * temp2 % MOD);
      }
    }

    return answer;
  }
}

public class MainClass {

  public record TestCase(int[][] mat);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      // [[1,2],[3,4]]
      new TestCase(ParseArray2D("[[1,2],[3,4]]")),
      // [[12345],[2],[1]]
      new TestCase(ParseArray2D("[[12345],[2],[1]]")),
      // [[10,20],[18,16],[17,14],[16,9],[14,6],[16,5],[14,8],[20,13],[16,10],[14,17]]
      new TestCase(ParseArray2D("[[10,20],[18,16],[17,14],[16,9],[14,6],[16,5],[14,8],[20,13],[16,10],[14,17]]")),
      // [[68916659],[263909215]]
      new TestCase(ParseArray2D("[[68916659],[263909215]]"))
    };

    foreach (var tc in testcases) {
      var result = solution.ConstructProductMatrix(tc.mat);
      PrintArray2D(result);
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

