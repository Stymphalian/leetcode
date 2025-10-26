// P3346 -  Maximum Frequency of an Element After Performing Operations I
// https://leetcode.com/problems/maximum-frequency-of-an-element-after-performing-operations-i/description/?envType=daily-question&envId=2025-10-21
// Difficulty: Medium
// Time Taken: 4 hours

public class Solution {

    static void PrintArray(List<int> nums) {
        foreach (var num in nums) {
            Console.Write("{0} ", num);
        }
        Console.WriteLine("");
    }

    public int SearchLeft(List<int> nums, int k, int target, int index, int left, int right) {
        if (left > right) {
            return index;
        }

        int mid = left + (right - left) / 2;

        if (target < nums[mid] - k) {
            return SearchLeft(nums, k, target, index, left, mid - 1);
        } else if (target > nums[mid] + k) {
            return SearchLeft(nums, k, target, index, mid + 1, right);
        } else {
            return SearchLeft(nums, k, target, mid, left, mid - 1);
        }
    }

    public int SearchRight(List<int> nums, int k, int target, int index, int left, int right) {
        if (left > right) {
            return index;
        }

        int mid = left + (right - left) / 2;

        if (target < nums[mid] - k) {
            return SearchRight(nums, k, target, index, left, mid - 1);
        } else if (target > nums[mid] + k) {
            return SearchRight(nums, k, target, index, mid + 1, right);
        } else {
            return SearchRight(nums, k, target, mid, mid + 1, right);
        }
    }

    // 1 4 5
    public int NumberOverlaps(List<int> nums, int k, int target) {
        int left = SearchLeft(nums, k, target, -1, 0, nums.Count - 1);
        int right = SearchRight(nums, k, target, -1, 0, nums.Count - 1);
        return right - left + 1;
    }

    public int MaxFrequencyI(int[] nums, int k, int numOperations) {
        Dictionary<int, int> freqs = [];
        int maxFreq = 0;
        foreach (var n in nums) {
            if (freqs.ContainsKey(n)) {
                freqs[n] += 1;
            } else {
                freqs[n] = 1;
            }
            maxFreq = Math.Max(freqs[n], maxFreq);
        }

        var sortedNums = nums.ToList();
        sortedNums.Sort();
        // PrintArray(sortedNums);

        int min = sortedNums[0] - k;
        int max = sortedNums[^1] + k;
        for (int target = min; target < max; target++) {
            if (!freqs.ContainsKey(target)) {
                freqs[target] = 0;
            }

            // numer overlaps with target
            int overlaps = NumberOverlaps(sortedNums, k, target);
            int count = freqs[target];
            // Debug.Assert(overlaps >= count);
            int diff = Math.Min(overlaps - count, numOperations);

            maxFreq = Math.Max(count + diff, maxFreq);
        }

        return maxFreq;
    }
}

public class MainClass {

    record Case(int[] Nums, int k, int numOperations);

    public static void Main(string[] args) {
        Solution s = new Solution();
        
        // Original MaxFrequency tests
        Console.WriteLine("=== Testing MaxFrequency ===");
        Case[] cases = {
            new Case([1,4,5], 1, 2),    // 2
            new Case([5, 11, 20, 20], 5, 1),  // 2
            new Case([37,30,37], 26, 1) // 3
        };
        for (int ci = 0; ci < cases.Length; ci++) {
            Case c = cases[ci];
            var answer = s.MaxFrequency(c.Nums, c.k, c.numOperations);
            Console.WriteLine(string.Format("Case {0}: {1}", ci + 1, answer));
        }
    }

};


// 1 1 1 5 5 5, k=1
//3 