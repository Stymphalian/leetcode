// 2075 Decode the Slanted Ciphertext
// https://leetcode.com/problems/decode-the-slanted-ciphertext/description/
// Difficulty: Medium
// Time Taken: 00:13:10

using System.Text;

public class Solution {
  public string DecodeCiphertext(string encodedText, int rows) {
    int cols = encodedText.Length / rows;

    StringBuilder answer = new();
    int startX = 0;
    int x = 0;
    int y = 0;
    while(startX < cols && y < rows && x < cols) {
      char c = encodedText[y*cols + x];
      answer.Append(c);
      x += 1;
      y += 1;
      if (y > rows-1) {
        y = 0;
        x = startX + 1;
        startX += 1;
      }
    }
    return answer.ToString().TrimEnd();
  }
}


public class MainClass {

  public record TestCase(string e, int r);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      new TestCase("ch   ie   pr", 3),
      new TestCase("iveo    eed   l te   olc", 4),
      new TestCase("coding", 1)
    };

    foreach (var tc in testcases) {
      var result = solution.DecodeCiphertext(tc.e, tc.r);
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

