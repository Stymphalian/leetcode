

public class Solution {
    const uint MOD = 1_000_000_000 + 7;

    public int[] ProductQueries(int n, int[][] queries) {
        uint n2 = (uint)n;
        List<uint> powers = new();
        uint power = 1;
        while (n2 > 0) {
            if ((n2 & 1) > 0) {
                powers.Add(power);
            }
            n2 = n2 >> 1;
            power *= 2;
        }

        List<int> answers = new();
        foreach (var query in queries) {
            var left = query[0];
            var right = query[1];

            UInt128 answer = 1;
            for (var index = left; index <= right; index++) {
                answer = (answer * powers[index]) % MOD;
            }

            answers.Add((int)answer);
        }

        return answers.ToArray();
    }
}

public class MainClass {
    record Case(int N, int[][] Queries);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            // new Case(15, [[0,1], [2,2], [0,3]]),
            // new Case(2, [[0,0]]),
            // new Case(919, [[5,5],[4,4],[0,1],[1,5],[4,6],[6,6],[5,6],[0,3],[5,5],[5,6],[1,2],[3,5],[3,6],[5,5],[4,4],[1,1],[2,4],[4,5],[4,4],[5,6],[0,4],[3,3],[0,4],[0,5],[4,4],[5,5],[4,6],[4,5],[0,4],[6,6],[6,6],[6,6],[2,2],[0,5],[1,4],[0,3],[2,4],[5,5],[6,6],[2,2],[2,3],[5,5],[0,6],[3,3],[6,6],[4,4],[0,0],[0,2],[6,6],[6,6],[3,6],[0,4],[6,6],[2,2],[4,6]]),
            new Case(508, [[0,6]])
        };

        foreach (var c in cases) {
            foreach (var q in s.ProductQueries(c.N, c.Queries)) {
                Console.Write("{0} ", q);
            }
            Console.WriteLine("");
        }
    }

}
