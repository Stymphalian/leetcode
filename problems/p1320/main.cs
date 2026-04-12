// 1320 Minimum Distance to Type a Word Using Two Fingers
// https://leetcode.com/problems/minimum-distance-to-type-a-word-using-two-fingers/description/
// Difficulty: Hard
// Time Taken: 00:29:58

public class Solution {
  Dictionary<char, (int, int)> LAYOUT = new() {
    {'A', (0,0)},
    {'B', (0,1)},
    {'C', (0,2)},
    {'D', (0,3)},
    {'E', (0,4)},
    {'F', (0,5)},
    {'G', (1,0)},
    {'H', (1,1)},
    {'I', (1,2)},
    {'J', (1,3)},
    {'K', (1,4)},
    {'L', (1,5)},
    {'M', (2,0)},
    {'N', (2,1)},
    {'O', (2,2)},
    {'P', (2,3)},
    {'Q', (2,4)},
    {'R', (2,5)},
    {'S', (3,0)},
    {'T', (3,1)},
    {'U', (3,2)},
    {'V', (3,3)},
    {'W', (3,4)},
    {'X', (3,5)},
    {'Y', (4,0)},
    {'Z', (4,1)},
  };

  public int distanceBetweenChars(char c1, char c2) {
    var (y1, x1) = LAYOUT[c1];
    var (y2, x2) = LAYOUT[c2];
    return Math.Abs(y2-y1) + Math.Abs(x2-x1); 
  }

  public int MinDist(string word, int index, char finger1, char finger2, Dictionary<(int,char,char),int> memo) {
    if (index >= word.Length) {return 0;}
    var key = (index, finger1, finger2);
    if (memo.ContainsKey(key)) {
      return memo[key];
    }

    char cand = word[index];
    int cost1 = distanceBetweenChars(finger1, cand);
    int cost2 = distanceBetweenChars(finger2, cand);
    int cand1 = int.MaxValue;
    int cand2 = int.MaxValue;
    if (cand != finger2) {
      cand1 = MinDist(word, index+1, cand, finger2, memo) + cost1;  
    }
    if (cand != finger1) {
      cand2 = MinDist(word, index+1, finger1, cand, memo) + cost2;  
    }
    int best = Math.Min(cand1, cand2);

    memo[key] = best;
    return best;
  }

  public int MinimumDistance(string word) {
    HashSet<char> characters = word.ToHashSet();
    Dictionary<(int, char, char), int> memo = [];

    int best = int.MaxValue;
    foreach(var finger1 in characters) {
      foreach(var finger2 in characters) {
        if (finger1 == finger2) {continue;}
        best = Math.Min(best, MinDist(word, 0, finger1, finger2, memo));
      }
    }
    return best;
  }
}

public class MainClass {

  public record TC(string e);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC("CAKE"),
      new TC("HAPPY"),
      new TC("YEAR"),
    ];
    foreach (var tc in testcases) {
      var result = solution.MinimumDistance(tc.e);
      Console.WriteLine(result);
    }
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

