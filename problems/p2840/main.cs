// 2840 Check if Strings Can be Made Equal With Operations II
// https://leetcode.com/problems/check-if-strings-can-be-made-equal-with-operations-ii/description/
// Difficulty: Medium
// Time Taken: 00:12:11


public class Solution {
  public bool CheckStrings(string s1, string s2) {
    List<char> s11 = [];
    List<char> s12 = [];
    List<char> s21 = [];
    List<char> s22 = [];
    for(int index = 0; index < s1.Length; index++) {
      if (index%2 == 0) {
        s11.Add(s1[index]);
        s21.Add(s2[index]);
      } else {
        s12.Add(s1[index]);
        s22.Add(s2[index]);
      }
    }

    s11.Sort();
    s12.Sort();
    s21.Sort();
    s22.Sort();
    return s11.SequenceEqual(s21) && s12.SequenceEqual(s22);
  }
}

public class MainClass {

  public record TestCase(string s1, string s2);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      new TestCase("abcdba", "cabdab"),
      new TestCase("abe", "bea"),
    };

    foreach (var tc in testcases) {
      var result = solution.CheckStrings(tc.s1, tc.s2);
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

