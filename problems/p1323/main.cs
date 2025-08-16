// Maximum 69 Number
// https://leetcode.com/problems/maximum-69-number/description/
public class Solution {
    public int Maximum69Number (int num) {
        int originalNum = num;
        int power10 = 1;
        int lastSixPower10 = -1;
        while (num > 0) {
            int digit = num % 10;
            num /= 10;

            if (digit == 6) {lastSixPower10 = power10;}
            power10 *= 10;
        }

        if (lastSixPower10 >= 0) {
            return originalNum - 6*lastSixPower10 + 9*lastSixPower10;
        }
        return originalNum;
    }
}

public class MainClass {
    record Case(int S);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(9669),
            new Case(9996),
            new Case(9999),
            new Case(9),
            new Case(6),
        };

        foreach (var c in cases) {
            Console.WriteLine(s.Maximum69Number(c.S));
        }
    }

}

