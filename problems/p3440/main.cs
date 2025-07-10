using System.Collections;

public class Solution {

    public int BisectLeft(List<int> nums, int target) {
        int left = 0;
        int right = nums.Count - 1;
        while (left <= right) {
            int mid = left + (right - left) / 2;
            if (nums[mid] < target) {
                left = mid + 1;
            } else {
                right = mid - 1;
            }
        }
        return left;
    }

    public int MaxFreeTime(int eventTime, int[] startTime, int[] endTime) {

        List<int> gaps = new();
        int prevEnd2 = 0;
        for (int i = 0; i < startTime.Length; i++) {
            int leftGap = startTime[i] - prevEnd2;
            gaps.Add(leftGap);
            prevEnd2 = endTime[i];
            // int prevEnd = (i == 0) ? 0 : endTime[i - 1];
            // int nextStart = (i == startTime.Length - 1) ? eventTime : startTime[i + 1];

            // int leftGap = startTime[i] - prevEnd;
            // int rightGap = nextStart - endTime[i];
            // gaps.Add(leftGap);
            // gaps.Add(rightGap);
        }
        gaps.Add(eventTime - endTime[endTime.Length - 1]);
        gaps.Sort();

        int biggestGap = 0;
        // foreach each meeting either shift the meeting or find an empty space
        // record the max gap generated
        for (int i = 0; i < startTime.Length; i++) {
            int prevEnd = (i == 0) ? 0 : endTime[i - 1];
            int nextStart = (i == startTime.Length - 1) ? eventTime : startTime[i + 1];
            int space = nextStart - prevEnd;
            int meeting = endTime[i] - startTime[i];
            int leftGap = startTime[i] - prevEnd;
            int rightGap = nextStart - endTime[i];
            
            int shiftedGap = space - meeting;
            int index = BisectLeft(gaps, meeting);
            int correction = ((leftGap >= meeting) ? 1 : 0) + ((rightGap >= meeting) ? 1 : 0) + 1;
            int movedGap = 0;
            if (gaps.Count - index >= correction) {
                movedGap = space;
            }
            
            int gap = Math.Max(shiftedGap, movedGap);
            biggestGap = Math.Max(biggestGap, gap);
        }
        return biggestGap;
    }

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