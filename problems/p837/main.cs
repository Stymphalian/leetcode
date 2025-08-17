// 837. New 21 Game
// https://leetcode.com/problems/new-21-game/description
// Time Taken: 00:50:49

using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Xml;

public class Solution {
    const double TOLERANCE = 1e-5;

    public double BottomUp(int n, int k, int maxCard) {
        int maxHand = n + maxCard;
        double[] dp = new double[maxHand+1];
        for (int hand = maxHand; hand >= k; hand--) {
            dp[hand] = (hand <= n) ? 1.0 : 0.0;
        }

        // Keep a running sum
        int left = k;
        int right = k + maxCard;
        double current = 0.0;
        for (int i = left; i < right; i++) {
            current += dp[i];
        }

        for (int hand = k - 1; hand >= 0; hand--) {
            double prob = current / maxCard;
            dp[hand] = prob;

            // Update the running sum
            current -= dp[hand + maxCard];
            current += dp[hand];
        }

        return dp[0];
    }

    public double New21Game(int n, int k, int maxCard) {
        return BottomUp(n, k, maxCard);
    }
}


public class MainClass {
    record Case(int N, int K, int maxPts);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(10, 1, 10),  // 1.0
            new Case(6,1, 10),    // 0.5
            new Case(21, 17, 10), // 0.73278
            new Case(5710, 5070, 5000), // 0.21283
            new Case(5710, 5070, 5500), // 0.25398
            new Case(5710, 5070, 8516),  // 0.13649
            new Case(0,0, 1)        // 1.0
        };

        foreach (var c in cases) {
            Console.WriteLine(s.New21Game(c.N, c.K, c.maxPts));
        }
    }

}

