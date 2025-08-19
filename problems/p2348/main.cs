// 2348: Number of Zero-Filled Subarrays
// Difficulty: Medium
// Time Taken: 01:57:38
// https://leetcode.com/problems/number-of-zero-filled-subarrays/description
// Sometiems I wonder why I even try.

using System.IO.Compression;
using System.Reflection;

public class Solution {

    public class SegTree {

        private record Item(bool isZeros, long left, long right, long count);
        private readonly int N;
        private readonly Item[] DP;

        public SegTree(int[] nums) {
            N = nums.Length;
            DP = new Item[2 * N - 1];
            Build(nums, 0, 0, nums.Length-1);
        }

        private int LeftIndex(int index, int left, int right) {
            return index + 1;
        }
        private int RightIndex(int index, int left, int right) {
            int mid = left + (right - left) / 2;
            int leftElems = mid - left + 1;
            int leftTreeSize = 2 * leftElems - 1;
            return index + leftTreeSize + 1;
        }

        private void Build(int[] nums, int index, int left, int right) {
            if (left == right) {
                int val = (nums[left] == 0) ? 1 : 0;
                DP[index] = new Item(val == 1, val, val, val);
            } else {
                int mid = left + (right - left) / 2;
                int leftIndex = LeftIndex(index, left, right);
                int rightIndex = RightIndex(index, left, right);

                Build(nums, leftIndex, left, mid);
                Build(nums, rightIndex, mid + 1, right);
                DP[index] = Combine(DP[leftIndex], DP[rightIndex]);
            }
        }

        private Item Combine(Item a, Item b) {
            bool allZeros = a.isZeros && b.isZeros;
            // int leftZeros = allZeros ? (a.left + b.right) : a.left;
            // int rightZeros = allZeros ? (a.left + b.right) : b.right;

            long leftZeros = 0;
            long rightZeros = 0;
            if (allZeros) {
                leftZeros = a.left + b.right;
                rightZeros = a.left + b.right;
            } else {
                if (a.isZeros) {
                    leftZeros = a.left + b.left;
                } else {
                    leftZeros = a.left;
                }
                if (b.isZeros) {
                    rightZeros = a.right + b.left;
                } else {
                    rightZeros = b.right;
                }
            }

            long count = a.count + b.count + (a.right * b.left);
            
            return new Item(
                a.isZeros && b.isZeros,
                leftZeros,
                rightZeros,
                count
            );
        }

        private Item SearchInternal(int index, int leftRange, int rightRange, int left, int right) {
            if (left > right) {
                return new Item(false, 0, 0, 0);
            }
            if (leftRange == left && rightRange == right) {
                return DP[index];
            }

            int mid = leftRange + (rightRange - leftRange) / 2;
            int leftIndex = LeftIndex(index, leftRange, rightRange);
            int rightIndex = RightIndex(index, leftRange, rightRange);

            Item leftValue = SearchInternal(
                leftIndex, leftRange, mid, left, Math.Min(mid, right));
            Item rightValue = SearchInternal(
                rightIndex, mid + 1, rightRange, Math.Max(mid + 1, left), right);
            return Combine(leftValue, rightValue);
        }
        
        public long Search(int left, int right) {
            return SearchInternal(0, 0, N - 1, left, right).count;
        }
    }

    public long Editorial(int[] nums) {
        long count = 0;
        long streak = 0;
        foreach (var n in nums) {
            if (n == 0) {
                streak += 1;
                count += streak;
            } else {
                streak = 0;
            }
        }
        return count;
    }

    public long ZeroFilledSubarray(int[] nums) {
        return Editorial(nums);
        // SegTree tree = new(nums);
        // return tree.Search(0, nums.Length - 1);
    }
}


public class MainClass {
    record Case(int[] nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([1,3,0,0,2,0,0,4]), // 6
            new Case([0,0,0,2,0,0]), // 9
            new Case([2,10,2019]), // 0
            new Case([0,0,0,0,0,0,0,-2,0,0,0,0,0,0,0]), // 56
            new Case([0,0,0,0,0,0,0,1]), // 28
        };

        foreach (var c in cases) {
            Console.WriteLine(s.ZeroFilledSubarray(c.nums));
        }
    }

}

