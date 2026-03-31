// 3474 Lexicographically Smallest Generated String
// https://leetcode.com/problems/lexicographically-smallest-generated-string/description/
// Difficulty: Hard
// Time Taken: 02:27:54

// Didn't solve. Use greedy approach from the editorial solution.
public class Solution {

  public string GenerateString(string str1, string str2) {
    int n = str1.Length;
    int m = str2.Length;
    int nm = n + m - 1;
    char[] final = new char[nm];
    bool[] recorded = new bool[nm];
    for(int index = 0; index < nm; index++) {
      final[index] = 'a';
    }
    
    // Satisfy all the T contraints
    bool copyInto(int offset, string target) {
      for(int i = 0; i < target.Length; i++) {
        int j = i + offset;
        if (recorded[j] && final[j] != target[i]) {
          return false;
        }
        final[j] = target[i];
        recorded[j] = true;     
      }
      return true;
    }
    for (int ni = 0; ni < n; ni++) {
      if (str1[ni] == 'T') {
        if (!copyInto(ni, str2)) {
          return "";
        }
      }
    }

    // Satisfy all the F contraints
    bool IsEqual(int offset, string target) {
      for(int index = 0; index < target.Length; index++) {
        int j = index + offset;
        if (final[j] != target[index]) {return false;}
      }
      return true;
    }
    bool UpdateRightmost(int offset, string target) {
      for(int index = target.Length-1; index >= 0; index--) {
        int j = index + offset;
        if (recorded[j]) {
          continue;
        }
        final[j] = 'b';
        return true;
      }
      return false;
    }
    for(int ni = 0; ni < n; ni++) {
      if (str1[ni] == 'F') {
        if (IsEqual(ni, str2)) {
          if (!UpdateRightmost(ni, str2)) {
            return "";
          }
        }
      }
    }

    return new string(final);
  }
}

public class MainClass {

  public record TestCase(string s1, string s2);

  public static void Main(string[] args) {
    Solution solution = new Solution();
    List<TestCase> testcases = new List<TestCase> {
      // new TestCase("TFTF", "ab"),
      // new TestCase("TFTF", "abc"),
      // new TestCase("F", "d"),
      new TestCase("TTFFT", "fff")
    };

    foreach (var tc in testcases) {
      var result = solution.GenerateString(tc.s1, tc.s2);
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

