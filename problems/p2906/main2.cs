public class Solution
{

  public int[][] ConstructProductMatrix(int[][] grid)
  {
    int height = grid.Length;
    int width = grid[0].Length;
    int area = width * height;

    const long MOD = 12345;
    int[][] answer = new int[height][];
    long[] leftToRight = new long[area];
    long[] rightToLeft = new long[area];
    for (int y = 0; y < height; y++)
    {
      answer[y] = new int[width];
    }

    int getIndex(int y, int x) { return y * width + x; }
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        int index = getIndex(y, x);
        long val = (index - 1 >= 0) ? leftToRight[index - 1] : 1;
        leftToRight[index] = val * grid[y][x] % MOD;
      }
    }
    for (int y = height - 1; y >= 0; y--)
    {
      for (int x = width - 1; x >= 0; x--)
      {
        int index = getIndex(y, x);
        long val = (index + 1 < area) ? rightToLeft[index + 1] : 1;
        rightToLeft[index] = val * grid[y][x] % MOD;
      }
    }

    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        int index = getIndex(y, x);
        long left = (index - 1 >= 0) ? leftToRight[index - 1] : 1;
        long right = (index + 1 < area) ? rightToLeft[index + 1] : 1;
        answer[y][x] = (int)(left * right % MOD);
      }
    }

    return answer;
  }
}

public class MainClass
{

  public record TestCase(int[][] mat);

  public static void Main(string[] args)
  {
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

    foreach (var tc in testcases)
    {
      var result = solution.ConstructProductMatrix(tc.mat);
      PrintArray2D(result);
    }
  }


  public static int[] ParseArray(string input)
  {
    var array = Array.ConvertAll(input.Trim('[', ']').Split(','), int.Parse);
    return array;
  }

  public static int[][] ParseArray2D(string input)
  {
    var rows = input.Trim('[', ']').Split("],[");
    var array = new int[rows.Length][];
    for (int i = 0; i < rows.Length; i++)
    {
      array[i] = Array.ConvertAll(rows[i].Split(','), int.Parse);
    }
    return array;
  }

  public static void PrintArray<T>(T[] array, string prefix = "")
  {
    Console.WriteLine($"{prefix}[{string.Join(", ", array)}]");
  }

  public static void PrintArray2D<T>(T[][] array)
  {
    Console.WriteLine("[");
    foreach (var row in array)
    {
      PrintArray(row, " ");
    }
    Console.WriteLine("]");
  }
};

