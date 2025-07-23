using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;

public class Solution {

    public int MaximumGain(string s, int x, int y) {
        char char1 = 'a';
        char char2 = 'b';
        int incr1 = x;
        int incr2 = y;
        if (y > x) {
            char1 = 'b';
            char2 = 'a';
            incr1 = y;
            incr2 = x;
        }


        int gain = 0;
        List<char> stack = [];
        for (int index = 0; index < s.Length; index++) {
            stack.Add(s[index]);
            while (stack.Count >= 2) {
                if (stack[^2] == char1 && stack[^1] == char2) {
                    gain += incr1;
                    stack.RemoveRange(stack.Count - 2, 2);
                } else {
                    break;
                }
            }
        }

        if (stack.Count > 0) {
            List<char> stack2 = [];
            for (int index = 0; index < stack.Count; index++) {
                stack2.Add(stack[index]);
                while (stack2.Count >= 2) {
                    if (stack2[^2] == char2 && stack2[^1] == char1) {
                        gain += incr2;
                        stack2.RemoveRange(stack2.Count - 2, 2);
                    } else {
                        break;
                    }
                }
            }
        }

        return gain;
    }
}

public class MainClass {

    static void PrintArray(int[] nums) {
        foreach (var num in nums) {
            Console.Write("{0} ", num);
        }
        Console.WriteLine("");
    }

    record Case(string S, int X, int Y);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case("cdbcbbaaabab", 4, 5),   // 19
            new Case("aabbaaxybbaabb", 5, 4), // 20
        };

        foreach (var c in cases) {
            Console.WriteLine(s.MaximumGain(c.S, c.X, c.Y));
        }
    }

}
