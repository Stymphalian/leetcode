using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;

public class Solution {

    public int DFS2(int[] nums, int totalScore, Dictionary<int, HashSet<int>> edges, int node, int parent, int A, ref int best) {
        int childrenScore = 0;
        foreach (var n in edges[node].ToList()) {
            if (n == parent) { continue; }
            int childScore = DFS2(nums, totalScore, edges, n, node, A, ref best);
            childrenScore ^= childScore;
        }

        int B = childrenScore ^ nums[node];
        if (parent != -1) {
            int C = totalScore ^ (A ^ B);
            int maxScore = Math.Max(A, Math.Max(B, C));
            int minScore = Math.Min(A, Math.Min(B, C));
            best = Math.Min(best, maxScore - minScore);
        }


        return B;
    }

    public int DFS(int[] nums, int totalScore, Dictionary<int, HashSet<int>> edges, int node, int parent, ref int best) {
        int childrenScore = 0;
        foreach (var n in edges[node].ToList()) {
            if (n == parent) { continue; }
            int childScore = DFS(nums, totalScore, edges, n, node, ref best);
            childrenScore ^= childScore;
        }
        int A = childrenScore ^ nums[node];

        // DFS2
        if (parent != -1) {
            edges[node].Remove(parent);
            edges[parent].Remove(node);
            DFS2(nums, totalScore, edges, parent, -1, A, ref best);
            edges[node].Add(parent);
            edges[parent].Add(node);
        }

        return A;
    }

    public int Editorial(int[] nums, int[][] edges) {
        int best = int.MaxValue;
        int totalTreeScore = 0;
        Dictionary<int, HashSet<int>> adj = new();
        for (int n = 0; n < nums.Length; n++) {
            totalTreeScore ^= nums[n];
            adj[n] = new();
        }
        foreach (var edge in edges) {
            adj[edge[0]].Add(edge[1]);
            adj[edge[1]].Add(edge[0]);
        }

        DFS(nums, totalTreeScore, adj, 0, -1, ref best);
        return best;
    }

    public int MinimumScore(int[] nums, int[][] edges) {
        return Editorial(nums, edges);
    }
}

public class MainClass {

    static void PrintArray(int[] nums) {
        foreach (var num in nums) {
            Console.Write("{0} ", num);
        }
        Console.WriteLine("");
    }

    record Case(int[] Nums, int[][] Edges);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([1,5,5,4,11], [[0,1],[1,2],[1,3],[3,4]]), // 9
            new Case([5,5,2,4,4,2], [[0,1],[1,2],[5,2],[4,3],[1,3]]),  // 0
            new Case([28,24,29,16,31,31,17,18], [[0,1],[6,0],[6,5],[6,7],[3,0],[2,1],[2,4]]), // 8
            new Case([9,14,2,1], [[2,3],[3,0],[3,1]]) // 11
        };

        foreach (var c in cases) {
            Console.WriteLine(s.MinimumScore(c.Nums, c.Edges));
        }
    }

}
