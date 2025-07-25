public class Solution {

    public int GetXorOfSubtree(
        int[] nums, int[][] edges, int node, int parentNode, Dictionary<int, Dictionary<int, int>> scores) {
        scores[node] = [];
        int nodeScore = nums[node];

        foreach (var edge in edges) {
            if (edge[0] == node && edge[1] != parentNode) {
                int score = GetXorOfSubtree(nums, edges, edge[1], node, scores);
                scores[node][edge[1]] = score;
                nodeScore ^= score;
            } else if (edge[1] == node && edge[0] != parentNode) {
                int score = GetXorOfSubtree(nums, edges, edge[0], node, scores);
                scores[node][edge[0]] = score;
                nodeScore ^= score;
            }
        }

        return nodeScore;
    }

    public void PushScores(
        int[] nums,
        int[][] edges,
        int node,
        int parentNode,
        int incomingScore,
        Dictionary<int, Dictionary<int, int>> scores)
    {

        if (parentNode != -1) {
            scores[node][parentNode] = incomingScore;    
        }

        int score = nums[node];
        int mergedScores = nums[node];
        foreach (var neighbor in scores[node]) {
            mergedScores ^= neighbor.Value;
        }

        foreach (var neighbor in scores[node]) {
            if (neighbor.Key != parentNode) {
                PushScores(nums, edges, neighbor.Key, node, mergedScores ^ neighbor.Value, scores);
            }
        }
    }


    public void CalculateScores(int[] nums, int[][] edges, Dictionary<int, Dictionary<int, int>> scores) {
        GetXorOfSubtree(nums, edges, 1, -1, scores);
        PushScores(nums, edges, 1, -1, 0, scores);
    }

    public int GetScore(int node, int exclude, int[] nums, Dictionary<int, Dictionary<int, int>> scores) {
        int score = nums[node];
        foreach (var neighbor in scores[node]) {
            if (neighbor.Key == exclude) { continue; }
            score ^= neighbor.Value;
        }
        return score;
    }

    public IEnumerable<(int n1, int n2)> DFS(Dictionary<int, Dictionary<int, int>> scores, int node, int parent) {
        foreach (var n in scores[node]) {
            if (n.Key != parent) {
                yield return (node, n.Key);
                foreach (var r in DFS(scores, n.Key, node)) {
                    yield return r;
                }
            }
        }
    }

    public int MinimumScore(int[] nums, int[][] edges) {
        int best = int.MaxValue;
        Dictionary<int, Dictionary<int, int>> scores = new();
        CalculateScores(nums, edges, scores);

        HashSet<(int, int)> tried = [];
        foreach (var edge1 in DFS(scores, 0, -1)) {
            int node1 = edge1.n1;
            int node2 = edge1.n2;
            int A = GetScore(node1, node2, nums, scores);
            int B = GetScore(node2, node1, nums, scores);

            // Console.WriteLine("---");
            // Console.WriteLine(edge1);
            // Console.WriteLine("Left:");
            foreach (var edge2 in DFS(scores, node1, node2)) {
                // Console.WriteLine(string.Format(" {0}, {1}", edge2.n1, edge2.n2));
                int node3 = edge2.n1;
                int node4 = edge2.n2;
                int C = GetScore(node3, node4, nums, scores);
                int D = GetScore(node4, node3, nums, scores);
                C ^= B;

                // Console.WriteLine(string.Format("  B={0},C={1},D={2}", B, C, D));
                int maxScore = Math.Max(B, Math.Max(C, D));
                int minScore = Math.Min(B, Math.Min(C, D));
                best = Math.Min(best, maxScore - minScore);
            }

            // Console.WriteLine("Right:");
            foreach (var edge2 in DFS(scores, node2, node1)) {
                // Console.WriteLine(string.Format(" {0}, {1}", edge2.n1, edge2.n2));
                int node3 = edge2.n1;
                int node4 = edge2.n2;
                int C = GetScore(node3, node4, nums, scores);
                int D = GetScore(node4, node3, nums, scores);
                C ^= A;

                int maxScore = Math.Max(A, Math.Max(C, D));
                int minScore = Math.Min(A, Math.Min(C, D));
                // Console.WriteLine(string.Format("  A={0},C={1},D={2}", A, C, D));
                best = Math.Min(best, maxScore - minScore);
            }
        }

        return best;
    }
}