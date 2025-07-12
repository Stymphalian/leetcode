using System.Collections;

public class Solution {
    public int CoinChange(int[] coins, int amount) {
        int[] dp = new int[amount + 1];
        for (int amnt = 0; amnt <= amount; amnt++) {
            dp[amnt] = (amnt % coins[0] == 0) ? (amnt / coins[0]) : int.MaxValue;;
        }

        for (int ci = 1; ci < coins.Length; ci++) {
            for (int amnt = coins[ci]; amnt <= amount; amnt++) {
                int a = (dp[amnt - coins[ci]] == int.MaxValue) ? int.MaxValue : dp[amnt - coins[ci]] + 1;
                int b = amnt % coins[ci] == 0 ? (amnt / coins[ci]) : int.MaxValue;
                int c = dp[amnt];
                dp[amnt] = Math.Min(Math.Min(a, b), c);
            }
        }

        return (dp[amount] == int.MaxValue) ? -1: dp[amount];
    }
};

public class MainClass {

    class Case {
        public int[] coins;
        public int amount;
    
        public Case(int[] coins, int amount) {
            this.coins = coins;
            this.amount = amount;
        }
    };

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new int[]{1,2,5}, 11),  // 3
            new Case(new int[]{2}, 3),       // -1
            new Case(new int[]{1}, 0),       // 0
            new Case(new int[]{2,5,10,1}, 27),  // 4
        };

        foreach (var c in cases) {
            Console.WriteLine(s.CoinChange(c.coins, c.amount));
        }
    }

}