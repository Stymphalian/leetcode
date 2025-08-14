// Easy
// 2264. Largest 3-Same-Digit Number in String
// https://leetcode.com/problems/largest-3-same-digit-number-in-string/description/

public class Solution
{

    public string LargestGoodInteger(string num)
    {
        int best = -1;
        for (int index = 0; index <= num.Length - 3; index++)
        {
            if (num[index] == num[index + 1] && num[index + 1] == num[index + 2])
            {
                if (best == -1)
                {
                    best = index;
                }
                else if (num[index] > num[best])
                {
                    best = index;
                }
            }
        }

        if (best == -1) { return ""; }
        return num[best..(best + 3)];
    }
}

public class MainClass {
    record Case(string S);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case("6777133339"),
            new Case("2300019"),
            new Case("42352338"),
            new Case("222")
        };

        foreach (var c in cases) {
            Console.WriteLine(s.LargestGoodInteger(c.S));
        }
    }

}

