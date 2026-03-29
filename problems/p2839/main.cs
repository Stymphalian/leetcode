// 2839 Check if Strings Can be Made Equal With Operations I
// https://leetcode.com/problems/check-if-strings-can-be-made-equal-with-operations-i/description/
// Difficulty: Easy
// Time Taken: 00:45:16


public class Solution {
  public bool CanBeEqual(string s1, string s2) {
    HashSet<char> s11 = [s1[0], s1[2]];
    HashSet<char> s12 = [s1[1], s1[3]];
    HashSet<char> s21 = [s2[0], s2[2]];
    HashSet<char> s22 = [s2[1], s2[3]];
    return s11.SetEquals(s21) && s12.SetEquals(s22);
  }
}

public class MainClass {

  public record TestCase(string s1, string s2);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      new TestCase("abcd", "cdab"),
      new TestCase("abcd", "dacb"),
    };

    foreach (var tc in testcases) {
      var result = solution.CanBeEqual(tc.s1, tc.s2);
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

