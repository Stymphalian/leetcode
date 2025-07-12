using System.Collections;

public class Solution {
    public int FindTargetSumWays(int[] nums, int target) {
        int capacity = nums.Sum();
        if (target > capacity || target < -capacity) {
            return 0;
        }

        int[] dp = new int[2*capacity + 1];
        int[] next_dp = new int[2*capacity + 1];
        for (int ti = 0; ti <= 2 * capacity; ti++) {
            int t = ti - capacity;
            dp[ti] += (t == nums[0]) ? 1 : 0;
            dp[ti] += (t == -nums[0]) ? 1 : 0;
        }

        for (int ni = 1; ni < nums.Length; ni++) {            
            for (int ti = 0; ti < dp.Length; ti++) {
                int posit = (ti - nums[ni] >= 0) ? dp[ti - nums[ni]] : 0;
                int minus = (ti + nums[ni] <= dp.Length-1) ? dp[ti + nums[ni]] : 0;
                next_dp[ti] = posit + minus;
            }

            Array.Copy(next_dp, dp, next_dp.Length);
        }

        return dp[target + capacity];
    }
}

public class MainClass {

    class Case {
        public int[] nums;
        public int target;
    
        public Case(int[] nums, int target) {
            this.nums = nums;
            this.target = target;
        }
    };

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new int[]{1,1,1,1,1,}, 3),  // 5
            new Case(new int[]{1}, 1),  // 1
            new Case(new int[]{1}, 2),  // 0
            new Case(new int[]{0}, 0),  // 2
        };

        foreach (var c in cases) {
            Console.WriteLine(s.FindTargetSumWays(c.nums, c.target));
        }
    }

}