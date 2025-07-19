using System.Collections;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;


public class Solution {
    public long MinimumDifference(int[] nums) {
        int n = nums.Length / 3;
        int mid = nums.Length / 2;

        // PQ are min-heaps
        PriorityQueue<int, int> maxPQ = new();
        PriorityQueue<int, int> minPQ = new();
        long[] minTrack = new long[nums.Length];
        long[] maxTrack = new long[nums.Length];
        long minSum = 0;
        long maxSum = 0;

        for (int i = 0; i < n-1; i++) {
            int j = nums.Length - i - 1;
            maxPQ.Enqueue(i, -nums[i]);
            minPQ.Enqueue(j, nums[j]);
            minSum += nums[i];
            maxSum += nums[j];
        }
        for (int i = n-1; i < nums.Length; i++) {
            minSum += nums[i];
            maxPQ.Enqueue(i, -nums[i]);
            if (maxPQ.Count > n) {
                int cand = maxPQ.Dequeue();
                minSum -= nums[cand];
            }
            
            minTrack[i] = minSum;
        }
        for (int i = nums.Length - n; i >= 0; i--) {
            maxSum += nums[i];
            minPQ.Enqueue(i, nums[i]);
            if (minPQ.Count > n) {
                int cand = minPQ.Dequeue();
                maxSum -= nums[cand];
            }        
            maxTrack[i] = maxSum;
        }


        long best = long.MaxValue;
        for (int i = n-1; i < nums.Length-n; i++) {
            long cand = minTrack[i] - maxTrack[i+1];
            best = Math.Min(best, cand);
        }
        return best;
    }
}

public class MainClass {

    record Case(int[] Nums);

    public static void Main(string[] args) {
        Solution s = new Solution();
        Case[] cases = {
            new Case([3,1,2]),    // -1 
            new Case([7,9,5,8,1,3]),  // 1
            new Case([5,7,3,7,10,2,6,9,7]), // -12
            new Case([16,46,43,41,42,14,36,49,50,28,38,25,17,5,18,11,14,21,23,39,23]), // -14
        };
        for (int ci = 0; ci < cases.Length; ci++) {
            Case c = cases[ci];
            var answer = s.MinimumDifference(c.Nums);
            Console.WriteLine(string.Format("{0}", answer));
        }
    }

};
