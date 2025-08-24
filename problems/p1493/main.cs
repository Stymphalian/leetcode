// 1493 Longest Subarray of 1's After Deleting One Element
// https://leetcode.com/problems/longest-subarray-of-1s-after-deleting-one-element/description/
// Difficulty: Medium
// Time: 00:10:15

public class Solution
{
    public int LongestSubarray(int[] nums)
    {
        if (nums.Length <= 1)
        {
            return 0;
        }

        int best = nums[0];
        int prev = 0;
        int current = nums[0];
        bool seen_zero = (nums[0] == 0);

        for (int index = 1; index < nums.Length; index++)
        {
            if (nums[index] == 0)
            {
                seen_zero = true;
                if (nums[index] == nums[index - 1])
                {
                    // Run of two zero, we can't join the run of 1s
                    prev = 0;
                }
                else
                {
                    // We can still return the run of 1's if we join this segment
                    // with the next segment of ones
                    prev = current;
                }
                current = 0;
            }
            else
            {
                current += 1;
            }

            best = Math.Max(best, prev + current);
        }

        if (seen_zero == false)
        {
            return best - 1;
        }
        return best;
    }
}

public class MainClass {
    record Case(int[] nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new int[] {1,1,0,1 }),
            new Case(new int[] {0,1,1,1,0,1,1,0,1}),
            new Case(new int[] {1,1,1}),
        };

        foreach (var c in cases) {
            Console.WriteLine(s.LongestSubarray(c.nums));
        }
    }
}

