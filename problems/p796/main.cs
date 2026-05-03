// 796 Rotate String
// https://leetcode.com/problems/rotate-string/description/
// Difficulty: Easy
// Time Taken: 00:04:51


public class Solution {
  public bool RotateString(string s, string goal) {
    if (s.Length != goal.Length) { return false; }

    for (int shift = 0; shift <= s.Length; shift++) {

      bool found = true;
      for (int index = 0; index < goal.Length; index++) {
        int left = (index + shift) % s.Length;
        int right = index;
        if (s[left] != goal[right]) {
          found = false;
          break;
        }
      }

      if (found) { return true; }
    }
    return false;
  }
}

public class Solution2 {
  // RabinKarp rolling hash algorithm

  public long Hash(string s, int len, int Base, int mod) {
    long h = 0;
    for(int index = 0; index < len; index++) {
      int intChar = s[index] - 'a';
      h = (h * Base + intChar) % mod;
    }
    return h;
  }

  public bool DoubleCheck(string s, string target, int offset) {
    for(int index = 0; index < target.Length; index++) {
      if (s[index + offset] != target[index]) {
        return false;
      }
    }
    return true;
  }

  public bool Contains(string source, string target) {
    int Base = 256;
    int M = target.Length;
    int N = source.Length;
    if (M > N) {return false;}
    int Mod = 5381; // Better to use a very large prime

    int BaseToPowerM = 1;
    for(int index = 1; index <= M-1; index++) {
      BaseToPowerM = (Base * BaseToPowerM) % Mod;
    }
    long targetHash = Hash(target, M, Base, Mod);
    long sourceHash = Hash(source, M, Base, Mod);

    // Early exit
    if (targetHash == sourceHash && DoubleCheck(source, target, 0)) {
      return true;
    }

    for(int index = M; index < N; index++) {
      // Remove leading character from the Hash
      int leading = source[index - M] - 'a';
      sourceHash = (sourceHash + Mod - (BaseToPowerM*leading % Mod)) % Mod;
      // Add the lagging character from the Hash
      int lagging = source[index] - 'a';
      sourceHash = (sourceHash * Base + lagging) % Mod;

      int offset = index - M + 1;
      if (targetHash == sourceHash && DoubleCheck(source, target, offset)) {
        return true;
      }
    }
    return false;
  }

  public bool RotateString(string s, string goal) {
    if (s.Length != goal.Length) { return false; }
    string s_goal = s + s;
    return Contains(s_goal, goal);   
  }
}



public class MainClass {

  public record TC(string a, string b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC("abcde", "cdeab"),
      new TC("abcde", "abced"),
    ];
    foreach (var tc in testcases) {
      var result = solution.RotateString(tc.a, tc.b);
      // PrintArray(result);
      Console.WriteLine(result);
    }
  }

  public static T[][] P2D<T>(T[,] arr) {
    int rows = arr.GetLength(0);
    int cols = arr.GetLength(1);
    T[][] output = new T[rows][];
    for (int row = 0; row < rows; row++) {
      output[row] = new T[cols];
      for (int col = 0; col < cols; col++) {
        output[row][col] = arr[row, col];
      }
    }
    return output;
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

