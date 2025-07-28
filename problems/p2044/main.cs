using System.Runtime.InteropServices;
using System.Security.AccessControl;

public class Solution {
    public int dfs(int[] nums, int index, int best, int current, Dictionary<(int,int), int> memo) {
        if (index == nums.Length) {
            return (current == best) ? 1 : 0;
        }
        var key = (index, current);
        if (memo.TryGetValue(key, out int value)) {
            return value;
        }
        int a = dfs(nums, index + 1, best, current | nums[index], memo);
        int b = dfs(nums, index + 1, best, current, memo);
        memo[key] = a + b;
        return memo[key];
    }

    public int CountMaxOrSubsets(int[] nums) {
        int best = nums.ToList().Aggregate(0, (acc, x) => acc | x);
        int count = dfs(nums, 0, best, 0, []);
        return count;
    }
}

public class MainClass {
    record Case(int[] Nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([3,1]),  // 2
            new Case([2,2,2]), // 7
            new Case([3,2,1,5]), // 6
            // new Case([1,2,3,4,5,6,7,8,9,1,2,3,4,5,6])
        };

        foreach (var c in cases) {
            Console.WriteLine(s.CountMaxOrSubsets(c.Nums));
        }
    }

}
