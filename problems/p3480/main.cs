public class Solution {

    public long totalSubarrays(long n) {
        return n * (n + 1) / 2;
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
