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

    public double Editorial(int n) {
        const double TOLERANCE = 1e-5;
        int m = (int)Math.Ceiling(n / 25.0);
        Dictionary<(int, int), double> dp = [];

        var func = (int a, int b) => {
            return (
                dp[(Math.Max(0, a - 4), b)] +
                dp[(Math.Max(0, a - 3), Math.Max(b - 1, 0))] +
                dp[(Math.Max(0, a - 2), Math.Max(b - 2, 0))] +
                dp[(Math.Max(0, a - 1), Math.Max(b - 3, 0))]
            ) / 4.0;
        };
        dp[(0, 0)] = 0.5; // n == 0, both finish at the same time.

        for (int a = 1; a <= m; a++) {
            dp[(0, a)] = 1.0;
            dp[(a, 0)] = 0.0;

            for (int b = 1; b <= a; b++) {
                dp[(b, a)] = func(b, a);
                dp[(a, b)] = func(a, b);
            }

            if ((1.0 - dp[(a, a)]) < TOLERANCE) {
                return 1.0;
            }
        }

        return dp[(m, m)];
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

