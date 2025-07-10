using System.Collections;

public class Solution {

    public int MaxFreeTime(int eventTime, int[] startTime, int[] endTime) {
        int n = startTime.Length;
        int biggestGap = 0;

        // Iterate left to right
        int prevEnd = 0;
        int biggestFreeLeftGap = 0;
        for (int i = 0; i < n; i++) {
            int next = (i == n - 1) ? eventTime : startTime[i + 1];
            int leftGap = startTime[i] - prevEnd;
            int rightGap = next - endTime[i];
            int meeting = endTime[i] - startTime[i];

            int mergedGap = leftGap + rightGap;
            int movedGap = leftGap + rightGap + meeting;
            int candidateGap = Math.Max(mergedGap, (biggestFreeLeftGap >= meeting) ? movedGap : 0);
            biggestGap = Math.Max(biggestGap, candidateGap);

            biggestFreeLeftGap = Math.Max(biggestFreeLeftGap, leftGap);
            prevEnd = endTime[i];
        }

        // Iterate right to left
        int nextStart = eventTime;
        int biggestFreeRightGap = 0;
        for (int i = n - 1; i >= 0; i--) {
            int prev = (i == 0) ? 0 : endTime[i - 1];
            int leftGap = startTime[i] - prev;
            int rightGap = nextStart - endTime[i];
            int meeting = endTime[i] - startTime[i];

            int mergedGap = leftGap + rightGap;
            int movedGap = leftGap + rightGap + meeting;
            int candidateGap = Math.Max(mergedGap, (biggestFreeRightGap >= meeting) ? movedGap : 0);
            biggestGap = Math.Max(biggestGap, candidateGap);

            biggestFreeRightGap = Math.Max(biggestFreeRightGap, rightGap);
            nextStart = startTime[i];
        }


        return biggestGap;
    }
}

public class MainClass {

    class Case {
        public int eventTime;
        public int[] startTime;
        public int[] endTime;

        public Case(int eventTime, int[] startTime, int[] endTime) {
            this.eventTime = eventTime;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    };

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(5, new int[]{1, 3}, new int[]{2, 5}),
            new Case(10, new int[]{0,7,9 }, new int[]{ 1, 8, 10}),
            new Case(10, new int[]{0,3,7,9 }, new int[]{1,4,8,10 }),
            new Case(5, new int[]{0,1,2,3,4 }, new int[]{1,2,3,4,5 }),
        };

        foreach (var c in cases) {
            Console.WriteLine(s.MaxFreeTime(c.eventTime, c.startTime, c.endTime));
        }
    }

}