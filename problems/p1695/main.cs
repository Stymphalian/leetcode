using System.Text;

public class Solution {
    public int MaximumUniqueSubarray(int[] nums) {
        Dictionary<int, int> seen = new();
        int[] runningSum = new int[nums.Length];
        Func<int, int> get = x => (x >= 0 && x < nums.Length) ? runningSum[x] : 0;

        int best = 0;
        int segmentSum = 0;
        int left = 0;
        for (int right = 0; right < nums.Length; right++) {
            int value = nums[right];
            runningSum[right] = get(right - 1) + value;
            
            if (seen.ContainsKey(value) && seen[value] >= left) {
                segmentSum = get(right) - get(seen[value]);
                left = seen[value] + 1;
                seen[value] = right;
            } else {
                segmentSum += value;
                seen[value] = right;
            }

            if (segmentSum > best) {
                best = segmentSum;
            }
        }

        return best;
    }
}

public class MainClass {

    static void PrintArray(int[] nums) {
        foreach (var num in nums) {
            Console.Write("{0} ", num);
        }
        Console.WriteLine("");
    }

    record Case(int[] Nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([4,2,4,5,6]),  // 17
            new Case([5,2,1,2,5,2,1,2,5]),  // 8
        };

        foreach (var c in cases) {
            Console.WriteLine(s.MaximumUniqueSubarray(c.Nums));
        }
    }

}
