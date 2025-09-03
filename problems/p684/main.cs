// 684 Redundant Connection
// https://leetcode.com/problems/redundant-connection/description/
// Difficulty: Medium
// Time Taken: 00:31:27

public class Solution {

    public class UnionFind {
        int[] parents;
        int[] rank;
        public UnionFind(int n) {
            parents = new int[n];
            rank = new int[n];
            for (int i = 0; i < n; i++) {
                parents[i] = i;
            }
        }

        public int Find(int a) {
            if (parents[a] == a) {
                return a;
            }
            // Compress the path when possible
            parents[a] = Find(parents[a]);
            return parents[a];
        }

        public void Union(int a, int b) {
            int pa = Find(a);
            int pb = Find(b);
            if (pa == pb) {
                return;
            }

            if (rank[pa] > rank[pb]) {
                parents[pb] = pa;
                rank[pa] += 1;
            } else {
                parents[pa] = pb;
                rank[pb] += 1;
            }
        }
    }

    public int[] FindRedundantConnection(int[][] edges) {
        int n = edges.Length;
        UnionFind uf = new(n);
        foreach (var edge in edges) {
            int a = edge[0]-1;
            int b = edge[1]-1;
            if (uf.Find(a) == uf.Find(b)) {
                return edge;
            }
            uf.Union(a, b);
        }
        return new int[2];
    }
}

public class MainClass {
    record Case(int[,] nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new int[,] { {1,2 }, {1,3 }, {2,3 } }), //2,3
            new Case(new int[,] { { 1, 2 } , { 2, 3 } , { 3, 4 } , { 1, 4 } , { 1, 5 } }), //1,4
            new Case(new int[,] { { 3, 4 } , { 1, 2 } , { 2, 4 } , { 3, 5 } , { 2, 5 } }), //2,5
            new Case(new int[,] { { 1, 5 } , { 3, 4 } , { 3, 5 } , { 4, 5 } , { 2, 4 } }), // 4,5
        };

        foreach (var c in cases) {

            int[][] caseDoubleArray = new int[c.nums.Length/2][];
            for (int i = 0; i < c.nums.Length/2; i++) {
                caseDoubleArray[i] = new int[2];
                caseDoubleArray[i][0] = c.nums[i, 0];
                caseDoubleArray[i][1] = c.nums[i, 1];
            }

            var result = s.FindRedundantConnection(caseDoubleArray);
            Console.WriteLine($"{result[0]} {result[1]}");
        }
    }
}

