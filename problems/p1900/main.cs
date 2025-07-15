// using Answer = (int Min, int Max);
// using Key = (int n, int p1, int p2);

using System.Collections;

public class Solution {

    public void PrintDepth(int depth) {
        for (var i = 0; i < depth; i++) {
            Console.Write(" ");
        }
    }

    public void PrintList(List<int> list) {
        for (var i = 0; i < list.Count; i++) {
            Console.Write(string.Format("{0} ", list[i]));
        }
        Console.WriteLine("");
    }

    (int Min, int Max) Solve(int n, int p1, int p2, int depth, ref Dictionary<(int n, int p1, int p2), (int Min, int Max)> memo) {
        // PrintDepth(depth);
        // Console.WriteLine(string.Format("n={0},p1={1},p2={2}", n, p1, p2));
        if (memo.ContainsKey((n, p1, p2))) {
            return memo[(n, p1, p2)];
        }
        bool is_even = n % 2 == 0;
        bool is_odd = n % 2 == 1;
        int half = is_even ? n / 2 : n / 2 + 1;
        int xp1 = n - p1 + 1;
        int xp2 = n - p2 + 1;

        if (p1 == xp2) {
            return (1, 1);
        }


        if (is_even && p1 > half && p2 > half) {
            return Solve(n, xp2, xp1, depth, ref memo);
        }
        else if (is_odd && p1 >= half && p2 >= half) {
            return Solve(n, xp2, xp1, depth, ref memo);
        }

        // Left side
        int bestMin = int.MaxValue - 1;
        int bestMax = 0;
        if (p1 <= half && p2 <= half) {

            int leftLimit = p1 - 1;
            int rightLimit = p2 - p1 - 1;
            for (int i = 0; i <= leftLimit; i++) {
                for (int j = 0; j <= rightLimit; j++) {
                    int nextP1 = i + 1;
                    int nextP2 = nextP1 + j + 1;
                    var cand = Solve(half, nextP1, nextP2, depth + 1, ref memo);
                    bestMin = Math.Min(cand.Min + 1, bestMin);
                    bestMax = Math.Max(cand.Max + 1, bestMax);
                }
            }
            return (bestMin, bestMax);
        }

        // Across the Center
        if (xp2 < p1) {
            // Flip the player positions
            return Solve(n, xp2, xp1, depth, ref memo);
        }

        int leftLimit2 = p1 -1;
        int rightLimit2 = (xp2 - p1 - 1);
        int spacing = (Math.Abs(xp2 - p2) - 1)/2 + (is_odd ? 1 : 0);
        for (int i = 0; i <= leftLimit2; i++) {
            for (int j = 0; j <= rightLimit2; j++) {
                int nextP1 = i + 1;
                int nextP2 = nextP1 + j + spacing + 1;
                var cand = Solve(half, nextP1, nextP2, depth + 1, ref memo);
                bestMin = Math.Min(cand.Min + 1, bestMin);
                bestMax = Math.Max(cand.Max + 1, bestMax);
            }
        }
        return (bestMin, bestMax);
    }

    public int[] EarliestAndLatest(int n, int p1, int p2) {
        Dictionary<(int n, int p1, int p2), (int Min, int Max)> memo = new();
        (int Min, int Max) ans = Solve(n, p1, p2, 0, ref memo);
        return [ans.Min, ans.Max];
    }
};

public class MainClass {

    record Case(int N, int Player1, int Player2);

    public static void Main(string[] args) {
        Solution s = new Solution();
        Case[] cases = {
            new Case(11, 2, 4),  // 3,4
            new Case(3, 2, 3),   // 2,2
            new Case(5, 1, 5),   // 1,1
            new Case(4, 1, 3),   // 2,2
            new Case(5, 1, 4)    // 2,2
        };
        for (int ci = 0; ci < cases.Length; ci++) {
            Case c = cases[ci];
            var answer = s.EarliestAndLatest(c.N, c.Player1, c.Player2);
            Console.WriteLine(string.Format("{0} {1}", answer[0], answer[1]));
        }
    }

};
