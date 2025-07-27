using System.Globalization;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class Solution {

    public long totalSubarrays(long n) {
        // return n * n + n - (n * (n + 1) / 2);
        return n * (n + 1) / 2;
    }
    public long numSubArraysContaining(int n, int a, int b) {
        int x = a;
        int y = n - b - 1;
        return 1 + x + y + (x * y);
    }

    public long brute2(int n, int[][] conflicts, int exclude) {
        long count = 0;
        for (int left = 0; left < n; left++) {
            for (int right = left; right < n; right++) {

                bool good = true;
                for (int pi = 0; pi < conflicts.Length; pi++) {
                    if (pi == exclude) { continue; }
                    var pair = conflicts[pi];
                    int start = pair[0] - 1;
                    int end = pair[1] - 1;
                    if (left <= start && start <= right && left <= end && end <= right) {
                        good = false;
                        break;
                    }
                }

                if (good) {
                    count += 1;
                }
            }
        }
        return count;
    }

    public long brute(int n, int[][] conflictingPairs) {
        long best = 0;
        for (int exclude = 0; exclude < conflictingPairs.Length; exclude++) {
            long want = brute2(n, conflictingPairs, exclude);
            best = Math.Max(best, want);
        }
        return best;
    }

    public long solve(int n, int[][] conflicts) {
        PriorityQueue<int[], int> pq = new();
        for (int i = 0; i < conflicts.Length; i++) {
            var pair = conflicts[i];
            if (pair[0] > pair[1]) {
                (pair[1], pair[0]) = (pair[0], pair[1]);
            }
            pq.Enqueue(pair, pair[1]);
        }

        long total = totalSubarrays(n);
        long blocked = 0;
        long maxProfit = 0;

        int farthestRight = 0;
        int prevFarthestRight = 0;
        long currentProfit = 0;

        while (pq.Count > 0) {
            var pair = pq.Dequeue();
            int col = pair[0];
            int row = pair[1];

            if (col > farthestRight) {
                currentProfit = 0;
                prevFarthestRight = farthestRight;
                farthestRight = col;
            } else if (col > prevFarthestRight) {
                prevFarthestRight = col;
            }

            var peek = (pq.Count > 0) ? pq.Peek() : [0, n + 1];
            long height = peek[1] - row;
            long width = farthestRight - prevFarthestRight;

            blocked += (farthestRight * height);
            currentProfit += (width * height);
            maxProfit = Math.Max(currentProfit, maxProfit);
        }

        return total - blocked + maxProfit;
    }

    public long MaxSubarrays(int n, int[][] conflictingPairs) {
        return solve(n, conflictingPairs);
        // long want = brute(n, conflictingPairs);
        // long got = solve(n, conflictingPairs);
        // if (want != got) {
        //     Console.WriteLine("got = {0}, want = {1}", got, want);
        // }
        // return want;
    }
}


public class MainClass {
    record Case(int N, int[][] ConflictingPairs);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(4, [[2,3],[1,4]]),  // 9
            new Case(8, [[2,4], [5,6]]),  // 26
            new Case(5, [[1,2],[2,5],[3,5]]), // 12
            new Case(5, [[1,4]]), // 15
            new Case(12, [[2,3],[4,5], [2,7], [3,10], [7,11]]), // 47
        };

        foreach (var c in cases) {
            Console.WriteLine(s.MaxSubarrays(c.N, c.ConflictingPairs));
        }
    }

}
