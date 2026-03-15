// 1415 The k-th Lexicographical String of All Happy Strings of Length n
// https://leetcode.com/problems/the-k-th-lexicographical-string-of-all-happy-strings-of-length-n/description/
// Difficulty: Medium
// Time Taken: 00:28:50


public class Solution {
  Dictionary<string, string[]> options = new() { ["a"] = ["b", "c"], ["b"] = ["a", "c"], ["c"] = ["a", "b"] };

  public int maxCombinations(int n) {
    if (n == 1) { return 3; }
    return maxCombinations(n - 1) * 2;
  }

  public string Generate(int max, int k, string last_letter) {
    if (max <= 1) {
      return "";
    }

    if (last_letter != "") {
      int max_half = max / 2;
      if (k <= max_half) {
        return options[last_letter][0] + Generate(max_half, k, options[last_letter][0]);
      } else {
        return options[last_letter][1] + Generate(max_half, k - max_half, options[last_letter][1]);
      }
    } else {
      int max_thirds = max / 3;
      if (k <= max_thirds) {
        return "a" + Generate(max_thirds, k, "a");
      } else if (k > max_thirds && k <= 2 * max_thirds) {
        return "b" + Generate(max_thirds, k - max_thirds, "b");
      } else {
        return "c" + Generate(max_thirds, k - 2 * max_thirds, "c");
      }
    }
  }

  public string GetHappyString(int n, int k) {
    int max = maxCombinations(n);
    if (k > max) {
      return "";
    }
    return Generate(max, k, "");
  }
}


public class MainClass {
  public static void Main(string[] args) {
    testcase1();
    testcase2();
    testcase3();
  }

  public static void testcase1() {
    Solution solution = new Solution();
    string result = solution.GetHappyString(1, 3);
    Console.WriteLine(result); // "c"
  }

  public static void testcase2() {
    Solution solution = new Solution();
    string result = solution.GetHappyString(1, 4);
    Console.WriteLine(result); // ""
  }

  public static void testcase3() {
    Solution solution = new Solution();
    string result = solution.GetHappyString(3, 9);
    Console.WriteLine(result); // "cab"
  }
};

