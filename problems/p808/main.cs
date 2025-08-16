// Soup Servings
// https://leetcode.com/problems/soup-servings/description

public class Solution
{

    public double Finish(int a, int b, Dictionary<(int, int), double> memo)
    {
        if (a <= 0 || b <= 0)
        {
            if (a <= 0 && b > 0)
            {
                return 1.0;
            }
            else if (a <= 0 && b <= 0)
            {
                return 0.5;
            }
            else
            {
                return 0.0;
            }
        }

        if (memo.TryGetValue((a, b), out double value))
        {
            return value;
        }

        double prob = 0.0;
        prob += Finish(a - 100, b, memo);
        prob += Finish(a - 75, b - 25, memo);
        prob += Finish(a - 50, b - 50, memo);
        prob += Finish(a - 25, b - 75, memo);
        memo[(a, b)] = prob / 4.0;
        return prob / 4.0;
    }

    public double SoupServings(int n)
    {
        if (n > 5000) { return 1.0; }
        return Finish(n, n, []);
        // return AFinish(n, n, []) + SameFinish(n, n, []) / 2.0;
    }
}


public class MainClass {
    record Case(int S);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(50), // 0.62500
            new Case(100), // 0.71875
            new Case(5000), // 1.0000
            new Case(660295675)
        };

        foreach (var c in cases) {
            Console.WriteLine(s.SoupServings(c.S));
        }
    }

}

