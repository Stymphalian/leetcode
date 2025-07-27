public class Solution {

    public int IntegerBreak(int n) {
        int[] dp = new int[n + 1];
        dp[0] = 0;
        dp[1] = 1;
        dp[2] = 1;

        for (int index = 3; index <= n; index++) {
            int best = 0;
            for (int j = 1; j < index; j++) {
                int cand = dp[index - j] * j;
                int cand2 = j * (index - j);
                best = Math.Max(best, Math.Max(cand, cand2));
            }
            dp[index] = best;
        }

        return dp[n];
    }
}

public class MainClass {
    record Case(int N);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(2), // 1
            new Case(10), // 10
            new Case(20),  // 1458
            new Case(58) 
        };

        foreach (var c in cases) {
            Console.WriteLine(s.IntegerBreak(c.N));
        }
    }

}
