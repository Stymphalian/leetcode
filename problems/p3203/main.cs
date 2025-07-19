using System.Collections;
using System.Numerics;
using System.Runtime.InteropServices;

public class Solution {

    // Editorial solution
    public int MaximumLength(int[] nums, int k) {
        int[,] dp = new int[k, k];
        int best = 0;
        foreach (int num in nums) {
            int mod = num % k;
            for (int prev = 0; prev < k; prev++) {
                dp[prev, mod] = dp[mod, prev] + 1;
                best = Math.Max(best, dp[prev, mod]);
            }
        }
        return best;
    }
}

public class MainClass {

    record Case(int[] Nums, int K);

    public static void Main(string[] args) {
        Solution s = new Solution();
        Case[] cases = {
            new Case([1,2,3,4,5], 2),    // 5 
            new Case([1,4,2,3,1,4], 3),  // 4
        };
        for (int ci = 0; ci < cases.Length; ci++) {
            Case c = cases[ci];
            var answer = s.MaximumLength(c.Nums, c.K);
            Console.WriteLine(string.Format("{0}", answer));
        }
    }

};
