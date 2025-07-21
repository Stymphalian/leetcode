using System.Text;

public class Solution {
    public string MakeFancyString(string s) {
        StringBuilder s2 = new();
        for (int i = 0; i < s.Length; i++) {
            if (s2.Length == 0 || i + 1 >= s.Length) {
                s2.Append(s[i]);
                continue;
            }
            if (s2[^1] == s[i] && s[i] == s[i + 1]) {
                continue;
            } else {
                s2.Append(s[i]);
            }
        }
        return s2.ToString();
    }
}

public class MainClass {

    static void PrintArray(int[] nums) {
        foreach (var num in nums) {
            Console.Write("{0} ", num);
        }
        Console.WriteLine("");
    }

    record Case(string Fancy);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case("leeetcode"),  // 5
            new Case("aaabaaaa"),  // 5
            new Case("aab"),
        };

        foreach (var c in cases) {
            Console.WriteLine(s.MakeFancyString(c.Fancy));
        }
    }

}
