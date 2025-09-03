
// 3027. Find the Number of Ways to Place People II
// https://leetcode.com/problems/find-the-number-of-ways-to-place-people-ii/description
// Difficulty: Hard
// Time Taken: 02:04:46
// This is the same as p3025 which I solved yesterday optimially.
// Probably why it took me 2 hours yesterday.

public class Solution
{
    public int NumberOfPairs(int[][] points)
    {
        // sort the point (by y-index, and then by x_index)
        var sortedPoints = points.ToList();
        sortedPoints.Sort((a, b) =>
        {
            // sort by y index
            if (a[1] == b[1])
            {
                // sort by x-index (ascending)
                return a[0].CompareTo(b[0]);
            }
            else
            {
                // sort by y-index (descending)
                return b[1].CompareTo(a[1]);
            }
        });


        int answer = 0;
        for (int index = 0; index < sortedPoints.Count - 1; index++)
        {

            var refPoint = sortedPoints[index];
            int limit = int.MaxValue - 1;
            for (int other = index + 1; other < sortedPoints.Count; other++)
            {
                var current = sortedPoints[other];
                if (current[0] < refPoint[0])
                {
                    continue;
                }
                if (current[0] >= limit)
                {
                    continue;
                }
                limit = current[0];
                answer += 1;
            }
        }

        return answer;
    }
}

public class MainClass {
    record Case(int[,] nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new int[,] { { 1, 1 }, { 2, 2 }, { 3, 3 } }), // 0
            new Case(new int[,] { { 6, 2 }, { 4, 4 }, { 2, 6 } }), // 2
            new Case(new int[,] { { 3, 1 }, { 1, 3 }, { 1, 1 } }), // 2
            new Case(new int[,] { { 0, 5 } , { 4, 5 } }), // 1
            new Case(new int[,] { { 0, 1 } , { 1, 3 } , {6,1}}), // 2
            new Case(new int[,] { { 0, 6 } , { 2, 3 } , { 1, 1 } , { 4, 2 } }), // 3
            new Case(new int[,] { { 1,1 }, {0,4 }, {6,3 }, {0,5 } }), // 3
        };

        foreach (var c in cases) {

            int[][] caseDoubleArray = new int[c.nums.Length/2][];
            for (int i = 0; i < c.nums.Length/2; i++) {
                caseDoubleArray[i] = new int[2];
                caseDoubleArray[i][0] = c.nums[i, 0];
                caseDoubleArray[i][1] = c.nums[i, 1];
            }

            Console.WriteLine(s.NumberOfPairs(caseDoubleArray));
        }
    }
}

